// POST: api/DecoderAPI
        [ResponseType(typeof(DecoderModel))]
        public async Task<IHttpActionResult> PostDecoderModel(DecoderModel decoderModel)
        {
            if (!ModelState.IsValid || ApplicationKey(decoderModel.key))
            {
                return BadRequest(ModelState);
            }

            // search db for existing entitys
            var result = await db.DecoderModels.Where(x => x.VinNumber == decoderModel.VinNumber).FirstOrDefaultAsync();

            // if no result found add to db
            if (result == null)
            {
                db.DecoderModels.Add(decoderModel);
                await db.SaveChangesAsync();
                return Created("DefaultApi", decoderModel);
            }
            // if result exists but Security code is diffrent add to db 
            if(result.SecurityCode != decoderModel.SecurityCode)
            {
                db.DecoderModels.Add(decoderModel);
                await db.SaveChangesAsync();
                return Created("DefaultApi", decoderModel);
            }
            // do nothing
            return Ok("Already exists");
        }
