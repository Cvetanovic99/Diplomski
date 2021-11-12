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
        private readonly IStorageService _storageService;
        private readonly IAsyncRepository<File> _fileReposiory;
        private readonly IAsyncRepository<FileType> _fileTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FileService(IStorageService storageService, IAsyncRepository<File> fileRepository, IMapper mapper, IAsyncRepository<FileType> fileTypeRepository, IUserRepository userRepository)
        {
            this._storageService = storageService;
            this._fileReposiory = fileRepository;
            this._fileTypeRepository = fileTypeRepository; ;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task<FileDto> UploadFile(CreateFileDto file, UserDto userDto)
        {
            var fileDto = await _storageService.UploadAsync(file.File);

            try
            {
                var userEntity = await _userRepository.GetByIdAsync(userDto.Id);//_mapper.Map<User>(userDto);
                await _fileReposiory.AddAsync(new File { FileName = fileDto.FileName, Path = fileDto.Path, Size = fileDto.Size, Owner = userEntity });

                var fileType = await _userRepository.GetUserFileTypes(userDto.Id, fileDto.Type);
                if (fileType == null)
                {
                    userEntity.FileTypes.Add(new FileType { Type = fileDto.Type, Count = 1 });
                    await _userRepository.UpdateAsync(userEntity);
                }
                else
                {
                    fileType.Count++;
                    await _fileTypeRepository.UpdateAsync(fileType);
                }

                return fileDto;
            }
            catch (Exception)
            {
                await _storageService.DeleteAsync(fileDto.Path);

                throw new ApiException("UnableToUploadFile", 500);
            }

            
        }
    }
}
