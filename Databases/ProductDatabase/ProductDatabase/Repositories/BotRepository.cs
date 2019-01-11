using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductDatabase.DTOs;
using ProductDatabase.Entities;

namespace ProductDatabase.Repositories
{
  public class BotRepository
  {
    private  ProductContext Db { get; }

    public BotRepository(ProductContext db)
    {
      Db = db;
    }

   
  }
}