using Leankit.DTO;
using Leankit.Entities;
using Leankit.EntitiesWrapper;

namespace Leankit.Interfaces
{
    public interface IApiCaller
    {
        IdentifierWrapper GetBoardIdentifiers(string boardId);
        AttributesWrapper GetBoardAttributes(string boardId);
        //string GetListOfItemsInBackLog(string boardId);
        //string GetListOfItemsInArchive(string boardId);
        //string GetListOfAllBoardsForAnAccount();
        //string AddCard(string boardId, string laneId, CardItem card);
        //string MoveCard(string boardId, string cardId, string laneId);
        //string UpdateCard(string boardId, string laneId, CardItem card);
    }
}
