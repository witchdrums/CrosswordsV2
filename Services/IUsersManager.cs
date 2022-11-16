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

    [ServiceContract(CallbackContract = typeof(IUsersManagerCallback))]
    public interface IUsersManager
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        bool FindUserByEmail(String userEmail);
        [OperationContract]
        Players Login(Users user);

        [OperationContract]
        Players GetPlayerInformation(Players player);
        [OperationContract]
        List<Categories> GetReportCategories();

        [OperationContract]
        bool RegisterReport(Reports report);
    }

    [ServiceContract]
    public interface IUsersManagerCallback
    {
        [OperationContract]
        void Response(bool response);
    }
}
