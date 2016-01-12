using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toolbox.Codetable.Business;
using Toolbox.Codetable.Controllers;
using Toolbox.Codetable.Entities;
using Toolbox.Codetable.Models;
using Toolbox.DataAccess.Exceptions;

namespace Toolbox.Codetable
{
    public class CodetabelControllerBase<TCodeTabelEntity, TCodeTabelModel> : ControllerBase
        where TCodeTabelEntity : CodetabelEntityBase
        where TCodeTabelModel : CodetabelModelBase
    {
        private readonly ICodetabelReader<TCodeTabelEntity> _reader;
        private readonly ICodetabelWriter<TCodeTabelEntity> _writer;

        public CodetabelControllerBase(IServiceCollection service) : base(service.BuildServiceProvider().GetService<ILogger>())
        {
            _reader = service.BuildServiceProvider().GetService<ICodetabelReader<TCodeTabelEntity>>();
            _writer = service.BuildServiceProvider().GetService<ICodetabelWriter<TCodeTabelEntity>>();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var codetabel = await _reader.GetAsync(id);
                if (codetabel == null)
                    return NotFoundResult($"Code met id {id} niet gevonden in {typeof(TCodeTabelModel).Name}.");
                var model = Mapper.Map<TCodeTabelModel>(codetabel);
                return OkResult(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Fout bij ophalen van {0} met id '{0}'", typeof(TCodeTabelModel).Name, id);
            }
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var values = await _reader.GetAllAsync();
                var mappedValues = Mapper.Map<IEnumerable<TCodeTabelModel>>(values);
                return OkResult(mappedValues);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Fout bij ophalen van {0}", typeof(TCodeTabelModel).Name);
            }
        }


        [HttpPost()]
        public async Task<IActionResult> InsertAsync([FromBody]TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);

            try
            {
                var entity = Mapper.Map<TCodeTabelEntity>(model);
                var insertedEntity = await _writer.InsertAsync(entity);

                return Created($"{Request.Path.Value}/{insertedEntity.Id}", Mapper.Map<TCodeTabelModel>(insertedEntity));
            }
            catch (CodetabelBusinessValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Fout bij inserten van {0}", typeof(TCodeTabelModel).Name);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);

            try
            {
                if (model == null) throw new CodetabelBusinessValidationException("model not provided");
                if (id != model.Id) throw new CodetabelBusinessValidationException("id does not match model id");

                var entity = Mapper.Map<TCodeTabelEntity>(model);
                await _writer.UpdateAsync(entity);
                return OkResult();
            }
            catch (EntityNotFoundException)
            {
                return NotFoundResult("Geen evenement gevonden met id {0}", id);
            }
            catch (CodetabelBusinessValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Fout bij updaten van {0}", typeof(TCodeTabelModel).Name);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _writer.DeleteAsync(id);
                return OkResult();
            }
            catch (EntityNotFoundException)
            {
                return NotFoundResult("Geen evenement gevonden met id {0}", id);
            }
            catch (CodetabelBusinessValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Fout bij deleten van {0}", typeof(TCodeTabelModel).Name);
            }
        }
    }
}
