﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Runtime.Serialization;
using BusinessLogic;

namespace Services
{

    [ServiceContract]
    public interface IUsersManager
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        bool FindUserByEmail(String userEmail);
        [OperationContract]
        Players Login(Users user);
        [OperationContract]
        bool RecoverPassword(Users user);
        [OperationContract]
        bool RegisterRecoveredPassword(Users user);
        [OperationContract]
        Players GetPlayerInformation(Players player);
        [OperationContract]
        List<Categories> GetReportCategories();

        [OperationContract]
        bool RegisterReport(Reports report);
        [OperationContract]
        Domain.Users GetUserByPlayer(Domain.Players player);

        [OperationContract]
        List<Users> GetReportedUsers();

        [OperationContract]
        bool UpdateUserBanStatus(Users user);

        [OperationContract]
        List<ProfileImages> GetProfileImages();
    }
}
