(function () { 
var myapp = angular.module('myApp', [])
    .controller('IndexRoles', IndexRoles)
    .controller('ServiceEq', ServiceEq)
    .controller('CompanyController', CompanyController)
    .controller('ALevel1', ALevel1)
    .controller('ALevel2', ALevel2)
    .controller('ALevel3', ALevel3)

    .service('dataService', dataService)
    .filter('bulidingAccess', bulidingAccessFactory); //AngularJS Filter to filter building by AccessLevel (Can be used for any collection to filter by a number);



// start dataService
function dataService($http) {
    var formdata = {};
    this.addIndexData = function (empName, empPhone, empEmail, jobRole, today, empStartDate, empRoleOther) {
        formdata.Full_Name = angular.copy(empName);
        formdata.Mobile = angular.copy(empPhone);
        formdata.Email = angular.copy(empEmail);
        formdata.Job_Role = angular.copy(jobRole);
        formdata.Created_Date = angular.copy(today);
        formdata.Start_Date = angular.copy(empStartDate);
        formdata.Hiring_Manager_ID = -1;
        formdata.Job_Others = angular.copy(empRoleOther);
        formdata.Updated_By = null;
        formdata.Updated_Date = null;
        formdata.Status = true;
        formdata.Today_Date = angular.copy(today);
    };
    this.addServiceData = function (selectedServices, selectedServiceOther, additionalService1) {
        formdata.Services_Equipment = angular.copy(selectedServices);
        formdata.Services_Equipment_Others = angular.copy(selectedServiceOther);
        formdata.Additional_Services = angular.copy(additionalService1);
    };

    this.addCompanyData = function (empCompany, empCompanyOther, companyAdditionalInfo) {
        formdata.Work_For = angular.copy(empCompany);
        formdata.Work_For_Others = angular.copy(empCompanyOther);
        formdata.Work_For_Additional_Info = angular.copy(companyAdditionalInfo);
    };

    this.addAL1Data = function (selectedBuildings1, selectedB1other, otherBuilding1) {
        formdata.Access_Level1 = angular.copy(selectedBuildings1);
        formdata.Access_Level1_Other = angular.copy(selectedB1other);
        formdata.Access_Level1_Restriction = angular.copy(otherBuilding1);
    };
    this.addAL2Data = function (selectedBuildings2, selectedB2other, otherBuilding2) {
        formdata.Access_Level2 = angular.copy(selectedBuildings2);
        formdata.Access_Level2_Other = angular.copy(selectedB2other);
        formdata.Access_Level2_Restriction = angular.copy(otherBuilding2);
    };
    this.addAL3Data = function (selectedBuildings3, selectedB3other) {
        formdata.Access_Level3 = angular.copy(selectedBuildings3.toString());
        formdata.Access_Level3_Other = angular.copy(selectedB3other);
    };

    this.postData = function () {
        $http.post(
            '/api/EMPLOYEE_INITIATION',
            JSON.stringify(formdata),
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        ).then(success, failure);
        function success() {
            location.href = '/Home/SubmitPage';
        }
        function failure(response) {
            console.log(response.data);
            alert("Error Submitting form! " + response.data.Message);
        }
    };
}
//-------------end dataService


//------ start IndexRoles Controller
function IndexRoles($scope, $http, dataService, bulidingAccessFilter) {
    $scope.jobroles = [];
    $scope.getJobs = function () {
        $http.get('/api/JOB_ROLE').then(function (jobsData) {
            $scope.jobroles = jobsData.data;
        });
    };
    $scope.getJobs();

    $scope.today = '';
    $scope.managerEmail = '';
    $scope.empName = '';


    $scope.empRole = {};
    $scope.empRole.selectedId = '';
    $scope.empRole.other = '';

    $scope.empEmail = '';
    $scope.empNum = '';
    $scope.empStartDate = '';

    $scope.goToServices = function () {
        dataService.addIndexData(angular.copy($scope.empName), $scope.empNum, $scope.empEmail, $scope.empRole.selectedId.toString(), $scope.today, $scope.empStartDate, $scope.empRole.other);
        location.href = '/Home/ServiceEquip';
    };
}
//-----end IndexRoles Controller

//======== start ServiceEq controller
function ServiceEq($scope, $http, dataService, bulidingAccessFilter) {

    $scope.services = [];
    $scope.getServices = function () {
        $http.get('/api/SERVICES_EQUIPMENT').then(function (servicesData) {
            $scope.services = servicesData.data;
        });
    };
    $scope.getServices();

    $scope.selectedServices = [];
    $scope.additionalService1 = '';

    $scope.selectedService = {};
    $scope.selectedService.selectedId = [];
    $scope.selectedService.other = '';

    $scope.updateSelected = function (selID) {
        var index = $scope.selectedServices.indexOf(selID);
        if (index > -1) {
            //remove
            $scope.selectedServices.splice(index, 1);
        }

        else {
            //add
            $scope.selectedServices.push(selID);
        }
    };

    function getIndexOfOtherInServices() {
        for (var i = 0; i < $scope.services.length; i++) {
            if ($scope.services[i].Service_Equipment === "Other:") {
                return $scope.services[i].Service_EquipmentID;
            }
        }
    }


    $scope.isOtherSelected = function () {
        var index = $scope.selectedServices.indexOf(getIndexOfOtherInServices());
        return index > -1 ? true : false;
    };

    $scope.gotoCompanies = function () {
        dataService.addServiceData($scope.selectedService, $scope.selectedService.other, $scope.additionalService1);
        location.href = '/Home/Company';
    };
}

//-----end ServiceEq Controller


//-----start companycontroller
function CompanyController($scope, $http, dataService, bulidingAccessFilter) {
    $scope.companies = [];
    $scope.companyAccess = [];

    $scope.getCompanies = function () {
        $http.get('/api/COMPANies').then(function (companiesData) {
            $scope.companies = companiesData.data;
        });
    };

    $scope.getCompanyAccess = function () {
        $http.get('/api/COMPANY_ACCESSLEVEL').then(function (companiesAccessData) {
            $scope.companyAccess = companiesAccessData.data;
        });
    };
    $scope.getCompanies();
    $scope.getCompanyAccess();

    $scope.empCompany = {};
    $scope.empCompany.selectedId = '';
    $scope.empCompany.other = '';

    $scope.companyAdditionalInfo = '';

    $scope.hasAccess = function (companyID, accessID) {
        for (var i = 0; i < $scope.companyAccess.length; i++) {
            if ($scope.companyAccess[i].CompanyID === companyID && $scope.companyAccess[i].Access_Level === accessID) {
                return true;
            }
        }
        return false;
    };

    $scope.gotoAL1 = function () {
        dataService.addCompanyData($scope.empCompany.selectedId, $scope.empCompany.other, $scope.companyAdditionalInfo);
        location.href = '/Home/AccessLevel1';
    };
    $scope.gotoAL2C = function () {
        dataService.addCompanyData($scope.empCompany.selectedId, $scope.empCompany.other, $scope.companyAdditionalInfo);
        location.href = '/Home/AccessLevel2';
    };
    $scope.gotoAL3C = function () {
        dataService.addCompanyData($scope.empCompany.selectedId, $scope.empCompany.other, $scope.companyAdditionalInfo);
        location.href = '/Home/AccessLevel3';
    };

    $scope.gotoAL3CSubmit = function () {
        dataService.addCompanyData($scope.empCompany.selectedId, $scope.empCompany.other, $scope.companyAdditionalInfo);
        location.href = '/Home/Submit';
    };


}
//-----end companycontroller



//------ start ALevel1 Controller
function ALevel1($scope, $http, dataService, bulidingAccessFilter) {

    $scope.buildingsAccess1 = [];

    $scope.getBuildings = function () {
        $http.get('/api/ACCESSLEVELs').then(function (bulidingsData) {
            $scope.buildingsAccess1 = bulidingAccessFilter(bulidingsData.data, 1);  // Filter Buidlings by AccessLevels
        });
    };
    $scope.getBuildings();


    $scope.selectedBuildings1 = [];
    $scope.otherBuilding1 = '';

    $scope.selectedB1 = {};
    $scope.selectedB1.selectedAccessID = [];
    $scope.selectedB1.other = '';

    $scope.updateSelectedBuilding1 = function (selID) {
        var index = $scope.selectedBuildings1.indexOf(selID);
        if (index > -1) {
            //remove
            $scope.selectedBuildings1.splice(index, 1);
        }

        else {
            //add
            $scope.selectedBuildings1.push(selID);
        }
    };

    function getIndexOfOtherInbuildingsAccess1() {
        for (var i = 0; i < $scope.buildingsAccess1.length; i++) {
            if ($scope.buildingsAccess1[i].Access_Area === "Other:") {
                return $scope.buildingsAccess1[i].AccessID;
            }
        }
    }


    $scope.isOtherBA1Selected = function () {
        var index = $scope.selectedBuildings1.indexOf(getIndexOfOtherInbuildingsAccess1());
        return index > -1 ? true : false;
    };

    $scope.gotoAL2 = function () {
        dataService.addAL1Data($scope.selectedB1.selectedAccessID, $scope.selectedB1.other, $scope.selectedB1.other);
        location.href = '/Home/AccessLevel2';
    };


}
//-----end ALevel1 Controller


//------ start ALevel2 Controller
function ALevel2($scope, $http, dataService, bulidingAccessFilter) {
    $scope.buildingsAccess2 = [];


    $scope.getBuildings = function () {
        $http.get('/api/ACCESSLEVELs').then(function (bulidingsData) {
            $scope.buildingsAccess2 = bulidingAccessFilter(bulidingsData.data, 2);
        });
    };

    $scope.getBuildings();


    $scope.selectedBuildings2 = [];
    $scope.otherBuilding2 = '';

    $scope.selectedB2 = {};
    $scope.selectedB2.selectedAccessID = [];
    $scope.selectedB2.other = '';

    $scope.updateSelectedBuilding2 = function (selID) {
        var index = $scope.selectedBuildings2.indexOf(selID);
        if (index > -1) {
            //remove
            $scope.selectedBuildings2.splice(index, 1);
        }

        else {
            //add
            $scope.selectedBuildings2.push(selID);
        }
    };

    function getIndexOfOtherInbuildingsAccess2() {
        for (var i = 0; i < $scope.buildingsAccess2.length; i++) {
            if ($scope.buildingsAccess2[i].Access_Area === "Other:") {
                return $scope.buildingsAccess2[i].AccessID;
            }
        }
    }


    $scope.isOtherBA2Selected = function () {
        var index = $scope.selectedBuildings2.indexOf(getIndexOfOtherInbuildingsAccess2());
        return index > -1 ? true : false;
    };

    $scope.gotoAL3 = function () {
        dataService.addAL2Data($scope.selectedB2.selectedAccessID, $scope.selectedB2.other, $scope.selectedB2.other);
        location.href = '/Home/AccessLevel3';
    };

}
//-----end ALevel2 Controller


//------ start ALevel3 Controller
function ALevel3($scope, $http, dataService, bulidingAccessFilter) {


    $scope.buildingsAccess3 = [];


    $scope.getBuildings = function () {
        $http.get('/api/ACCESSLEVELs').then(function (bulidingsData) {
            $scope.buildingsAccess3 = bulidingAccessFilter(bulidingsData.data, 3);
        });
    };

    $scope.getBuildings();


    $scope.selectedBuildings3 = [];
    $scope.otherBuilding3 = '';

    $scope.selectedB3 = {};
    $scope.selectedB3.selectedAccessID = [];
    $scope.selectedB3.other = '';

    $scope.updateSelectedBuilding3 = function (selID) {
        var index = $scope.selectedBuildings3.indexOf(selID);
        if (index > -1) {
            //remove
            $scope.selectedBuildings3.splice(index, 1);
        }

        else {
            //add
            $scope.selectedBuildings3.push(selID);
        }
    };

    function getIndexOfOtherInbuildingsAccess3() {
        for (var i = 0; i < $scope.buildingsAccess3.length; i++) {
            if ($scope.buildingsAccess3[i].Access_Area === "Other:") {
                return $scope.buildingsAccess3[i].AccessID;
            }
        }
    }

    $scope.isOtherBA3Selected = function () {
        var index = $scope.selectedBuildings3.indexOf(getIndexOfOtherInbuildingsAccess3());
        return index > -1 ? true : false;
    };


    $scope.submitForm = function () {
        dataService.addAL3Data($scope.selectedB3.selectedAccessID, $scope.selectedB3.other);
        dataService.postData();
    };
    $scope.submitForms = function () {
        dataService.postData();
    };

}
//-----end ALevel3 Controller







//-------------------- Filter Factory
function bulidingAccessFactory() {
    return function (input, accessLevel) {
        var out = [];
        for (var i = 0; i < input.length; i++) {
            if (input[i].Access_Level === accessLevel) {
                out.push(input[i]);
            }
        }
        return out;
    };
}
//---------------------------------


})();





















//var data = {  
    //    "Full_Name": $scope.empName,
    //    "Job_Role":$scope.empRole.selectedId,
    //    "Email":$scope.empEmail,
    //    "Mobile":$scope.empNum,
    //    "Start_Date":$scope.empStartDate,
    //    "Job_Others": $scope.empRole.other,
    //    "Services_Equipment":$scope.selectedServices,
    //    "Services_Equipment_Others":$scope.selectedService.other,
    //    "Additional_Services":$scope.additionalService1,
    //    "Work_For": $scope.empCompany,
    //    "Work_For_Others":$scope.empCompany.other,
    //    "Work_For_Additional_Info": $scope.companyAdditionalInfo,
    //    "Access_Level1":$scope.selectedBuildings1,
    //    "Access_Level1_Other":$scope.selectedB1.other,
    //    "Access_Level1_Restriction":$scope.otherBuilding1,
    //    "Access_Level2":$scope.selectedBuildings2,
    //    "Access_Level2_Other":$scope.selectedB2.other,
    //    "Access_Level2_Restriction":$scope.otherBuilding2,
    //    "Access_Level3": $scope.selectedBuildings3,
    //    "Access_Level3_Other": $scope.selectedB3.other,
    //    "Today_Date": $scope.today,
    //    "Hiring_Manager_ID": -1,
    //    "Created_Date": $scope.today,
    //    "Updated_By":'',
    //    "Updated_Date":null,
    //    "Status":true
//};
    //console.log(data);
    //$http.post(
    //    '/api/EMPLOYEE_INITIATION',
    //    JSON.stringify(data),
    //    {
    //        headers: {
    //            'Content-Type': 'application/json'
    //        }
    //    }
    //).then(success, failure);
    //function success() {
    //    location.href = 'SubmitPage';
    //}
    //function failure(response) {
    //    console.log(response.data);
    //}

