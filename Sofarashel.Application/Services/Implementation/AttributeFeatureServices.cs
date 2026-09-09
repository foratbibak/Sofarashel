using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sofarashel.Application.Services.Implementation
{
    public class AttributeFeatureServices(IGenericRepository<AttributeFeature> genericAttributeRepository) : IAttributeFeatureServices
    {
        public async Task<AttributeFeature> GetOrCreateAsync(string title, string value)
        {
            var existing = (await genericAttributeRepository.FindAsync(a =>
                a.AttributTitle == title && a.AttributValue == value))
                .FirstOrDefault();

            if (existing != null)
            {
                return existing;
            }

            var attribute = new AttributeFeature
            {
                AttributTitle = title,
                AttributValue = value,
                CreatDate = DateTime.Now,
                IsDelete = false
            };

            await genericAttributeRepository.AddAsync(attribute);
            await genericAttributeRepository.SaveAsync();

            return attribute;
        }

        public async Task<IEnumerable<AttributeFeature>> SearchAsync(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await genericAttributeRepository.FindAsync(a => !a.IsDelete);
            }

            return await genericAttributeRepository.FindAsync(a =>
                !a.IsDelete && (a.AttributTitle.Contains(keyword) || a.AttributValue.Contains(keyword)));
        }

        public async Task<AttributeFeature?> GetByIdAsync(int id)
        {
            return await genericAttributeRepository.SelectAsync(a => a.Id == id && !a.IsDelete);
        }

        public async Task<AttributeFeature?> DeleteFromLibraryAsync(int id)
        {
            var attribute = await genericAttributeRepository.SelectAsync(a => a.Id == id && !a.IsDelete);
            if (attribute is null)
            {
                return null;
            }

            attribute.IsDelete = true;
            attribute.DeleteDate = DateTime.Now;
            genericAttributeRepository.Update(attribute);
            await genericAttributeRepository.SaveAsync();

            return attribute;
        }
    }
}