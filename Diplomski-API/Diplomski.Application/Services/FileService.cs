using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Application.Exceptions;
using Diplomski.Application.Interfaces;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Core.Entities;
using Diplomski.Core.Entities.ManyToManyRelations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageService         _storageService;
        private readonly IFileRepository         _fileRepository;
        private readonly IFileTypeRepository     _fileTypeRepository;
        private readonly IUserRepository         _userRepository;
        private readonly IUserFileTypeRepository _userFileTypeRepository;
        private readonly IMapper                 _mapper;

        public FileService(IStorageService storageService, IFileRepository fileRepository, IMapper mapper, IFileTypeRepository fileTypeRepository, IUserRepository userRepository, IUserFileTypeRepository userFileTypeRepository)
        {
            this._storageService          = storageService;
            this._fileRepository          = fileRepository;
            this._fileTypeRepository      = fileTypeRepository;
            this._userFileTypeRepository  = userFileTypeRepository;
            this._userRepository          = userRepository;
            this._mapper                  = mapper;
        }

        public async Task<FileDto> UploadFile(CreateFileDto file, UserDto userDto)
        {
            var fileDto = await _storageService.UploadAsync(file.File);

            try
            {
                var userEntity   = await _userRepository.GetByIdAsync(userDto.Id);
                var userFileType = await _userFileTypeRepository.DoesUserContainFileTypeAsync(userDto.Id, fileDto.Type);

                if (userFileType != null)
                {
                    await _fileRepository.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = userFileType.FileType });
                    userFileType.Count++;
                    await _userFileTypeRepository.UpdateAsync(userFileType);
                }
                else 
                {
                    var fileType = await _fileTypeRepository.GetByTypeAsync(fileDto.Type);

                    if (fileType != null)
                    {
                        await _userFileTypeRepository.AddAsync(new UserFileType { Count = 1, User = userEntity, FileType = fileType });
                        await _fileRepository.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = fileType });
                    }
                    else
                    {
                        //Trebalo bi da se vrati recimo neki exception da ne podrzavamo taj tip fajla ali u ovom slucaju kreiramo taj tip fajla
                        await _fileTypeRepository.AddAsync(new FileType { Type = fileDto.Type });
                        var createdFileType = await _fileTypeRepository.GetByTypeAsync(fileDto.Type);
                        await _userFileTypeRepository.AddAsync(new UserFileType { Count = 1, User = userEntity, FileType = createdFileType });
                        await _fileRepository.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = createdFileType });
                    }
                }

                return fileDto;
            }
            catch (Exception)
            {
                await _storageService.DeleteAsync(fileDto.Path);

                throw new ApiException("UnableToUploadFile", 500);
            }
            //try
            //{
            //var userEntity = await _userRepository.GetByIdAsync(userDto.Id);
            //-------------------------------------------------
            //var fileType = await _possesdRepository.DosUserContainFileType(userEntity.Id);
            //if (ima)
            //{
            //ubaci fajl
            //apdejtuj possesd

            //} else
            //{
            //VratiFileType
            //if(postojiFileType)
            //{ 
            //kreiraj posessed za usera i filetype i svai count na 1
            //i kreiraj file sa tog usera i sa taj fileType
            //} else {
            //kreiraj fileType
            //vrati ga
            //kreiraj posessed za usera i filetype i stavi count na 1
            //i kreiraj file sa tog usera i sa taj fileType
            //}
            //}
            //--------------------------------------------------------------------------------------
            //await _fileRepository.AddAsync(new File { FileName = fileDto.FileName, Path = fileDto.Path, Size = fileDto.Size, Owner = userEntity });

            //    var fileType = await _userRepository.GetUserFileTypes(userDto.Id, fileDto.Type);//Trebalo bi da se menja da ide kroz fileType repository
            //    if (fileType == null)
            //    {
            //        userEntity.FileTypes.Add(new FileType { Type = fileDto.Type, Count = 1 });
            //        await _userRepository.UpdateAsync(userEntity);
            //    }
            //    else
            //    {
            //        fileType.Count++;
            //        await _fileTypeRepository.UpdateAsync(fileType);
            //    }

            //    return fileDto;
            //}
            //catch (Exception)
            // {
            //  await _storageService.DeleteAsync(fileDto.Path);

            // throw new ApiException("UnableToUploadFile", 500);
            // }
        }

        public async Task<List<FileExtradataDto>> GetUserFilesAsync(UserDto user, PaginationParameters paginationParameters)
        {
            List<FileExtradataDto> files = new List<FileExtradataDto>();
            if (paginationParameters.FileType is null)
            {
                var fileTypes = await _userFileTypeRepository.GetAllUserFileTypesAsync(user.Id);

                foreach (var fileType in fileTypes)
                {
                    FileExtradataDto fileExtradata;
                    fileExtradata = await _fileRepository.GetFileExtradataAsync(paginationParameters, user.Id, fileType.FileType.Type);
                    fileExtradata.Count = fileType.Count;
                    files.Add(fileExtradata);
                }

                return files;
            }
            else
            {
                files.Add(await _fileRepository.GetFileExtradataAsync(paginationParameters, user.Id, paginationParameters.FileType));
                return files;
            }
        }
    }
}
