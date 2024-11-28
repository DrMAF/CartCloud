//using Domain.DTO;
//using Domain.DTO.Core;
//using Domain.DTO.ECommerceManage;
//using Domain.Entities.ECommerceManage;
//using Application.Services.Impl;

//namespace Application.Services.Interfaces.ECommerceManage;

//public interface IECommerceSettingService
//{
//    Task<ResponseResult> GetIndexData();
//    Task<ResponseResult> AddAsync(ECommerceSettingDto model);
//    Task<ResponseResult> DeleteAsync(int id, DeleteTypeEnum deleteType = DeleteTypeEnum.Soft);
//    Task<ResponseResult> GetAllAsync();
//    Task<IReadOnlyList<ECommerceSetting>> GetAllForDropDown();
//    Task<ResponseResult> GetAllPaging(PagingBaseRequest modelRequset);
//    Task<ResponseResult> GetById(int id);
//    Task<ResponseResult> Update(ECommerceSettingDto modelDto);
//    Task<ResponseResult> GetDataTablePaging(JQDTParams model);
//}