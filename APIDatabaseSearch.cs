[HttpGet]
        public async Task<IHttpActionResult> get(string vin)
        {
            if(vin.Length != 17 && vin.All(char.IsLetterOrDigit))
            {
                return BadRequest();
            }

            DecoderModel decoder = await db.DecoderModels.Where(v => v.VinNumber == vin).FirstOrDefaultAsync();

            if (decoder == null)
            {
                return NotFound();
            }

            return Ok(decoder);
        }
