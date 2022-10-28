using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Validation;
using BusinessServices;



namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]

    public partial class ServicesImplementation : IUsersManager
    {
        
        CrosswordsContext context = new CrosswordsContext();
        public bool AddUser(Users user)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool result = userManagement.RegisterUser(user);
            return result;
        }

        public bool FindUserByEmail(string userEmail)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool result = userManagement.FindUserByEmail(userEmail);
            return result;
        }

        public Users FindUserByUserNameAndPassword(Users user)
        {
            BusinessLogic.User businessLogicUser = new BusinessLogic.User();
            businessLogicUser.username = user.username;
            businessLogicUser.password = user.password;
            if (context.Users.Contains(businessLogicUser))
            {
                businessLogicUser = context.Users.Find(businessLogicUser);
                user.username = businessLogicUser.username;
                user.credential = true;
            }
            else
            {
                user.credential = false;
            }

            return user;
        }

    }

    public partial class ServicesImplementation : IMessages
    {
        ConnectionMap usersMap = new ConnectionMap();
        public void SendChatMessage(List<Users> room, Users userOrigin, string message)
        {
            foreach (Users user in room)
            {
                usersMap.GetOperationContextForId(user.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin,message);
            }
        }

        public void SendPrivateMessage(Users userOrigin, Users userDestination, string message)
        {
            usersMap.GetOperationContextForId(userDestination.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin,"[Private] "+message);
        }


    }
    public partial class ServicesImplementation : IGameRoomManagement
    {
        RoomMap roomMap = new RoomMap();
        public int CreateRoom(Users user)
        {
            roomMap.NewRoom(user.idUser);//Todo Generate Random ID
            return user.idUser;
        }

        public void JoinToRoom(int idRoom,Users newUser)
        {
            roomMap.AddUserToRoom(idRoom, newUser);
            List<Users> usersRoom = roomMap.GetUsersInRoom(idRoom);
            foreach (Users user in usersRoom)
            {
                usersMap.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().UpdateRoom(usersRoom);
            }            
        }

        public void SendInvitationToRoom(int idRoom, Users userTarget)
        {
            usersMap.GetOperationContextForId(userTarget.idUser).GetCallbackChannel<IGameRoomManagementCallback>().ReciveInvitationToRoom(idRoom);
        }
    }

}
