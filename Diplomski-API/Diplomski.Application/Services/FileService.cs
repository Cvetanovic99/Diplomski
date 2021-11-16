using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Application.Exceptions;
using Diplomski.Application.Interfaces;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageService            _storageService;
        private readonly IFileRepository            _fileRepository;
        private readonly IFileTypeRepository        _fileTypeRepository;
        private readonly IUserRepository            _userRepository;
        private readonly IMapper                    _mapper;

        public FileService(IStorageService storageService, IFileRepository fileRepository, IFileTypeRepository fileTyperepository, IMapper mapper, IUserRepository userRepository)
        {
            this._storageService     = storageService;
            this._fileRepository     = fileRepository;
            this._fileTypeRepository = fileTyperepository;
            this._userRepository     = userRepository;
            this._mapper             = mapper;
        }

        public async Task<FileDto> UploadFile(CreateFileDto file, UserDto userDto)
        {
            var fileDto = await _storageService.UploadAsync(file.File);

            try 
            {
                var fileType = await _fileTypeRepository.GetByTypeAsync(fileDto.Type);
                if (fileType is null)
                {
                    throw new ApiException("Api do not support that type of file", 500);
                }
                else
                {
                    var userEntity = await _userRepository.GetByIdAsync(userDto.Id);
                    await _fileRepository.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = fileType.Type });
                }

                return fileDto;
            }
            catch 
            {
                await _storageService.DeleteAsync(fileDto.Path);
                throw new ApiException("Unable to upload file", 500);
            }
        }

        public async Task<List<FileExtradataDto>> GetUserFilesAsync(UserDto user, PaginationParameters paginationParameters)
        {
            List<FileExtradataDto> files = new List<FileExtradataDto>();
            if (paginationParameters.FileType is null)
            {
                var fileTypes = await _fileRepository.GetAllUserFileTypesAsync(user.Id);

                foreach (var fileType in fileTypes)
                {
                    FileExtradataDto fileExtradata;
                    fileExtradata = await _fileRepository.GetFileExtradataAsync(paginationParameters, user.Id, fileType);
                    fileExtradata.Count = await _fileRepository.GetCountOfUserFileType(fileType, user.Id);
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
