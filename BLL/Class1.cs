//using Application.Helpers.Auth;
//using Application.Helpers.Services;
//using Application.Interfaces;
//using Application.Services.Interfaces.ECommerceManage;
//using AutoMapper;
//using Domain.DTO;
//using Domain.DTO.Core;
//using Domain.DTO.ECommerceManage;
//using Domain.Entities.Banks;
//using Domain.Entities.ECommerceManage;
//using Domain.Entities.Settings;
//using Domain.Entities.Warehouse;
//using E_Commerce.Application.Services.Interface.Manage;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Localization;
//using SignalRSwaggerGen.Attributes;
//using System.Data;

//namespace Application.Services.Impl.ECommerceManage;

//[SignalRHub]
//public class ECommerceSettingService : IECommerceSettingService
//{

//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IMapper _mapper;
//    private readonly JWTHelper _jwtHelper;
//    private readonly IStringLocalizer<ECommerceSettingService> _localizer;
//    private readonly int _branchId;
//    private readonly ICashService _cashService;
//    private readonly IReadECommerceSettingService _service;

//    public ECommerceSettingService(
//        IUnitOfWork unitOfWork,
//        IMapper mapper,
//        JWTHelper jwtHelper,
//        ICashService cashService,
//        IStringLocalizer<ECommerceSettingService> localizer,
//        IReadECommerceSettingService service)
//    {
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//        _jwtHelper = jwtHelper;
//        _localizer = localizer;
//        _cashService = cashService;
//        var _userName = _jwtHelper.ReadTokenClaim(TokenClaimType.UserName);
//        _branchId = _cashService.GetDate<int>($"User-{_userName}-branchId");
//        _service = service;
//    }

//    public async Task<ResponseResult> DeleteAsync(int id, DeleteTypeEnum deleteType = DeleteTypeEnum.Soft)
//    {
//        var entity = await _unitOfWork.Repository<ECommerceSetting>().Where(e => e.Id == id).FirstOrDefaultAsync();

//        _unitOfWork.Repository<ECommerceSetting>().Remove(entity, IsHardDeleted: false);

//        await _unitOfWork.CompleteAsync();

//        return new ResponseResult { IsSuccess = true, Message = "Success" };

//    }

//    public async Task<ResponseResult> GetIndexData()
//    {
//        var eCommerceSettingEntity = await _unitOfWork.Repository<ECommerceSetting>()
//           .Include(a => a.ItemsSections)
//           .FirstOrDefaultAsync();

//        if (eCommerceSettingEntity == null)
//        {
//            await _service.InitializeECommerceSetting();
//            //return new ResponseResult { IsSuccess = false, Message = "ECommerce Setting NotFound" };

//            eCommerceSettingEntity = await _unitOfWork.Repository<ECommerceSetting>()
//             .Include(a => a.ItemsSections)
//             .FirstOrDefaultAsync();
//        }

//        var eCommerceSetting = _mapper.Map<ECommerceSettingDto>(eCommerceSettingEntity);

//        var branch = await _unitOfWork.Repository<Branch>().ToListAsync();
//        var bank = await _unitOfWork.Repository<Bank>().ToListAsync();
//        var storage = await _unitOfWork.Repository<Storage>().ToListAsync();
//        var treasury = await _unitOfWork.Repository<Treasury>().ToListAsync();

//        object obj = new
//        {
//            eCommerceSetting,
//            branch,
//            bank,
//            storage,
//            treasury,
//        };
//        return new ResponseResult { IsSuccess = true, Obj = obj };
//    }

//    public async Task<ResponseResult> GetAllAsync()
//    {
//        var list = await _unitOfWork.Xtra_GenericRepository<ECommerceSetting>().Where(e => e.IsActive == true).ToListAsync();
//        if (list == null)
//            return new ResponseResult { IsSuccess = false, Message = "NotFound" };

//        return new ResponseResult
//        {
//            IsSuccess = true,
//            Message = "success",
//            Obj = list,
//        };
//    }

//    public async Task<ResponseResult> AddAsync(ECommerceSettingDto model)
//    {
//        //var branch = await _unitOfWork.Repository<Branch>().FirstOrDefaultAsync(a => a.Id == model.BranchId);
//        //if (branch == null)
//        //    return new ResponseResult { IsSuccess = false, Message = "خطا في التعرف علي الفرع!" };

//        //var entity = _mapper.Map<ECommerceSetting>(model);

//        //entity.BranchId = branch.Id;
//        //entity.BranchName = branch.NameAr;

//        //await _unitOfWork.Repository<ECommerceSetting>().AddAsync(entity);
//        //var x = await _unitOfWork.CompleteAsync();

//        return new ResponseResult { IsSuccess = false, Message = "not allawed" };

//    }

//    public async Task<ResponseResult> GetAllPaging(PagingBaseRequest modelRequset)
//    {
//        if (modelRequset == null)
//        {
//            return new ResponseResult { IsSuccess = false, Message = "PageNumber Required ; PageSize Required" };
//        }
//        if (modelRequset.PageNumber <= 0)
//        {
//            modelRequset.PageNumber = 1;
//        }
//        if (modelRequset.PageSize <= 0)
//        {
//            modelRequset.PageSize = 1;
//        }

//        try
//        {
//            var data = await _unitOfWork.Repository<ECommerceSetting>().GetAllAsync(modelRequset.PageNumber, modelRequset.PageSize,
//                        x => x.OrderByDescending(
//                        z => z.No));

//            var res = new Paging<List<ECommerceSettingDto>>(_mapper.Map<List<ECommerceSettingDto>>(data.Data),
//               data.TotalPages, data.CurrentPage, data.PageSize, data.TotalItems);

//            return new ResponseResult
//            {
//                IsSuccess = true,
//                Message = "success",
//                Obj = res,
//            };
//        }
//        catch (Exception ex)
//        {
//            return null;
//        }
//    }

//    public async Task<ResponseResult> GetById(int id)
//    {
//        var modelIsExist = await _unitOfWork.Repository<ECommerceSetting>()
//            .Include(a => a.ItemsSections)
//            .FirstOrDefaultAsync();

//        if (modelIsExist == null)
//        {
//            if (id == 0)
//                await _unitOfWork.Repository<ECommerceSetting>().AddAsync(new ECommerceSetting());
//            else
//                return new ResponseResult { IsSuccess = false, Message = "ECommerce Setting NotFound" };
//        }

//        var result = _mapper.Map<ECommerceSettingDto>(modelIsExist);

//        return new ResponseResult
//        {
//            IsSuccess = true,
//            Message = "success",
//            Obj = result,
//        };
//    }

//    public async Task<ResponseResult> Update(ECommerceSettingDto modelDto)
//    {
//        var branch = await _unitOfWork.Repository<Branch>().FirstOrDefaultAsync(a => a.Id == _branchId);
//        if (branch == null)
//            return new ResponseResult { IsSuccess = false, Message = "خطا في التعرف علي الفرع!" };

//        var modelExist = await _unitOfWork.Repository<ECommerceSetting>()
//            .Include(a => a.ItemsSections)
//            .FirstOrDefaultAsync(a => a.Id == modelDto.Id);

//        if (modelExist == null)
//            return new ResponseResult { IsSuccess = false, Message = "NotFound" };

//        _mapper.Map(modelDto, modelExist);

//        await _unitOfWork.Repository<ECommerceSetting>().UpdateAsync(modelExist);

//        await _unitOfWork.CompleteAsync();

//        return new ResponseResult
//        {
//            IsSuccess = true,
//            Message = "success",
//            Obj = modelDto,
//        };
//    }

//    public async Task<IReadOnlyList<ECommerceSetting>> GetAllForDropDown()
//    {
//        var list = await _unitOfWork.Repository<ECommerceSetting>().Select(
//            e => new ECommerceSetting
//            {
//                Id = e.Id,
//                NameAr = e.NameAr,
//            }
//        ).ToListAsync();
//        return list;
//    }
//    public async Task<ResponseResult> GetDataTablePaging(JQDTParams model)
//    {
//        var count = await _unitOfWork.Repository<ECommerceSetting>().CountAsync(a => a.IsActive == true);

//        var data = await _unitOfWork.Repository<ECommerceSetting>().Where(a => a.NameAr.Contains(model.searchValue ?? ""))
//            .Skip(model.start).Take(model.length).ToListAsync();

//        var countfilterd = data.Count();

//        var returnData = new
//        {
//            model.draw,
//            recordsTotal = count,
//            recordsFiltered = countfilterd,
//            data,
//        };

//        return new ResponseResult { IsSuccess = true, Obj = returnData };
//    }

//}