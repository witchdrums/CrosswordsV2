using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public class RoomMap
    {
        private Dictionary<int, List<Users>> roomMap = new Dictionary<int, List<Users>>();

        public void NewRoom(int idRoom)
        {
            roomMap.Add(idRoom, new List<Users>());
        }
        public void AddUserToRoom(int idRoom,Users user)
        {
            roomMap[idRoom].Add(user);
        }
        public List<Users> GetUsersInRoom(int idRoom)
        {
            return roomMap[idRoom];
        }
        public void DeleteRoom(int idRoom)
        {
            roomMap.Remove(idRoom);
        }
        public void UpdateList(int idRoom,List<Users> usersList)
        {
            roomMap[idRoom] = usersList;
        }
    }
}
