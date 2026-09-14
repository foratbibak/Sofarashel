using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.AttributeFeatures;
using Sofarashel.Domain.ViewModels.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sofarashel.Application.Services.Implementation
{
    public class AttributeFeatureServices(IGenericRepository<AttributeFeature> genericAttributeRepository) : IAttributeFeatureServices
    {
        public async Task<AttributeFeatureViewModel> GetOrCreateAsync(string title, string value)
        {
            var attribute = (await genericAttributeRepository.FindAsync(a =>
                a.AttributeTitle == title && a.AttributeValue == value))
                .FirstOrDefault();

            if (attribute == null)
            {
                attribute = new AttributeFeature
                {
                    AttributeTitle = title,
                    AttributeValue = value,
                    CreatDate = DateTime.Now,
                    IsDelete = false
                };

                await genericAttributeRepository.AddAsync(attribute);
                await genericAttributeRepository.SaveAsync();
            }

            return AttributeFeatureMapper.MapToViewModel(attribute);
        }

        public async Task<IEnumerable<AttributeFeatureViewModel>> SearchAsync(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var attributes = await genericAttributeRepository.GetAllAsync();
                return AttributeFeatureMapper.MapToViewModelList(attributes);
            }

            var filteredAttributes = await genericAttributeRepository.FindAsync(attribute =>
                attribute.AttributeTitle.Contains(keyword) || attribute.AttributeValue.Contains(keyword));
            return AttributeFeatureMapper.MapToViewModelList(filteredAttributes);
        }

        public async Task<AttributeFeatureViewModel?> GetByIdAsync(int id)
        {
            var attribute = await genericAttributeRepository.GetByIdAsync(id);

            if (attribute == null)
            {
                return null;
            }

            return AttributeFeatureMapper.MapToViewModel(attribute);
        }

        public async Task<AttributeFeatureViewModel?> UpdateAsync(int id, string title, string value)
        {
            var attribute = await genericAttributeRepository.GetByIdAsync(id);

            if (attribute == null)
            {
                return null;
            }

            attribute.AttributeTitle = title;
            attribute.AttributeValue = value;
            attribute.UpdateDate = DateTime.Now;

            genericAttributeRepository.Update(attribute);
            await genericAttributeRepository.SaveAsync();

            return AttributeFeatureMapper.MapToViewModel(attribute);
        }

        public async Task<AttributeFeatureViewModel?> DeleteFromLibraryAsync(int id)
        {
            var attribute = await genericAttributeRepository.GetByIdAsync(id);

            if (attribute == null)
            {
                return null;
            }

            attribute.IsDelete = true;
            attribute.DeleteDate = DateTime.Now;
            genericAttributeRepository.Update(attribute);
            await genericAttributeRepository.SaveAsync();

            return AttributeFeatureMapper.MapToViewModel(attribute);
        }
    }
}