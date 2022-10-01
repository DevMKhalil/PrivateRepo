using CSharpFunctionalExtensions;
using MiniSalesApp.Application.Materials.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSalesApp.Logic.MaterialAgreget
{
    public class Material
    {
        public Material()
        {

        }

        public int MaterialId { get; internal set; }
        public string Name { get; internal set; }
        public int Code { get; internal set; }
        public decimal SellPrice { get; internal set; }
        public decimal PurchasePrice { get; internal set; }

        public static Result<Material> CreateMaterial(MaterialDto materialDto)
        {
            var res = ValidateMaterial(materialDto);

            if (res.IsFailure)
                return Result.Failure<Material>(res.Error);

            var material = new Material()
            {
                Name = materialDto.Name,
                Code = materialDto.Code,
                SellPrice = materialDto.SellPrice,
                PurchasePrice = materialDto.PurchasePrice
            };

            return Result.Success(material);
        }

        public static Result ValidateMaterial(MaterialDto materialDto)
        {
            if (string.IsNullOrEmpty(materialDto.Name))
                return Result.Failure(Messages.NameIsRequired);

            if (materialDto.Code <= 0)
                return Result.Failure(Messages.CodeIsRequired);

            if (materialDto.SellPrice <= 0)
                return Result.Failure(Messages.SellPriceIsRequired);

            if (materialDto.PurchasePrice <= 0)
                return Result.Failure(Messages.PurchasePriceIsRequired);

            return Result.Success();
        }

        public Result<Material> UpdateMaterial(MaterialDto materialDto)
        {
            var res = ValidateMaterial(materialDto);

            if (res.IsFailure)
                return Result.Failure<Material>(res.Error);

            Name = materialDto.Name;
            Code = materialDto.Code;
            SellPrice = materialDto.SellPrice;
            PurchasePrice = materialDto.PurchasePrice;

            return Result.Success(this);
        }
    }
}
