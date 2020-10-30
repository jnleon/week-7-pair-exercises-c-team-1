using System;
using System.Collections.Generic;
using CatCards.DAO;
using CatCards.Models;
using CatCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatCards.Controllers
{

    [Route("/api/cards")]
    [ApiController]

    public class CatController : ControllerBase
    {
        private readonly ICatCardDAO cardDAO;
        private readonly ICatFactService catFactService;
        private readonly ICatPicService catPicService;

        public CatController(ICatCardDAO _cardDAO, ICatFactService _catFact, ICatPicService _catPic)
        {
            catFactService = _catFact;
            catPicService = _catPic;
            cardDAO = _cardDAO;
        }

        [HttpGet]

        public List<CatCard> Get()
        {
            return cardDAO.GetAllCards();
        }

        [HttpGet("{id}")]

        public CatCard GetById(int Id)
        {
            return cardDAO.GetCard(Id);
        }

        [HttpGet("random")]

        public ActionResult<CatCard> GetRandomCard()
        {
            CatFactService serviceFact = new CatFactService();
            CatPicService servicePic = new CatPicService();

            CatCard card = new CatCard();

            CatFact fact = serviceFact.GetFact();
            CatPic pic = servicePic.GetPic();

            if(fact==null || pic == null)
            {
                return StatusCode(500);
            }

            card.CatFact = fact.Text;
            card.ImgUrl = pic.File;

            return Ok(card);
            
        }

        [HttpPost]

        public CatCard SaveCard(CatCard newCard)
        {
            return cardDAO.SaveCard(newCard);
        }

        [HttpPut("{Id}")]

        public ActionResult<CatCard> UpdateCard(int Id, CatCard catCard)
        {
            CatCard existing = cardDAO.GetCard(Id);
            if(existing == null)
            {
                return NotFound("Card not found");
            }

            bool result = cardDAO.UpdateCard(catCard);
            if (result)
            {
                return Ok(cardDAO.GetCard(Id));
            }
            return null;
        }

        [HttpDelete("{Id}")]

        public bool DeleteCard(int Id)
        {
            bool deleted = cardDAO.RemoveCard(Id);
            return deleted;
        }
    }
}
