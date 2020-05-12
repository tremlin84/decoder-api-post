[HttpPost]
        public async Task<IHttpActionResult> SortDB()
        {
            List<DecoderModel> model = new List<DecoderModel>();

            foreach (var decoder in db.DecoderModels)
            {
                model.Add(decoder);                                 
            }

            var duplicates = model.GroupBy(i => i.VinNumber)
                     .Where(x => x.Count() > 1)
                     .Select(val => val.FirstOrDefault());

            if (duplicates.Count() > 0)
            {
                foreach (var d in duplicates)
                {
                    db.DecoderModels.Remove(d);
                    await db.SaveChangesAsync();
                }
                return Ok();   
            }
            return InternalServerError();
        }
