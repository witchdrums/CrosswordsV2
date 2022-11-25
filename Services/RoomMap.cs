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
        private Dictionary<int, bool> roomMapStatus = new Dictionary<int, bool>();

        public void NewRoom(int idRoom)
        {
            roomMap.Add(idRoom, new List<Users>());
            roomMapStatus.Add(idRoom, true);
        }
        public void AddUserToRoom(int idRoom,Users user)
        {
            roomMap[idRoom].Add(user);
        }
        public void RemoveUserToRoom(int idRoom,Users user)
        {
            List<Users> oldUsersList = roomMap[idRoom];
            List<Users> updatedUsersList = new List<Users>();
            foreach (Users updatedUser in oldUsersList)
            {
                if (updatedUser.idUser != user.idUser)
                {
                    updatedUsersList.Add(updatedUser);
                }
            }
            roomMap[idRoom] = updatedUsersList;
        }
        public List<Users> GetUsersInRoom(int idRoom)
        {
            return roomMap[idRoom];
        }
        public void DeleteRoom(int idRoom)
        {
            roomMap.Remove(idRoom);
            roomMapStatus.Remove(idRoom);
        }
        public void UpdateList(int idRoom,List<Users> usersList)
        {
            roomMap[idRoom] = usersList;
        }

        public bool IsFullRoom(int idRoom)
        {
            bool response = false;
            if(roomMap[idRoom].Count() >= 4)
            {
                response = true;    
            }
            return response;
        }

        public bool ExistRoom(int idRoom)
        {
            bool response = false;
            if (roomMap.ContainsKey(idRoom))
            {
                response = true;
            }
            return response;
        }
        public bool IsEmptyRoom(int idRoom)
        {
            bool response = false;
            if (roomMap[idRoom].Count() == 0)
            {
                response = true;
            }
            return response;
        }

        public bool isRoomAvailable(int idRoom)
        {
            bool response = false;
            if (roomMapStatus[idRoom] == true)
            {
                response = true;
            }
            return response;
        }

        public void makeRoomUnavailable(int idRoom)
        {
            roomMapStatus[idRoom] = false;
        }
    }
}
