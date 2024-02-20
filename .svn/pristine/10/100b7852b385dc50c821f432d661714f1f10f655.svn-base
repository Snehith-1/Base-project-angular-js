
(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeEditController', SysMstEmployeeEditController);

        SysMstEmployeeEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

      

    function SysMstEmployeeEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        var perm_address1,perm_address2,perm_country,perm_state,perm_city,perm_postalcode;
        $scope.btncopy = function () {
            perm_address1 = $scope.txtperaddressline1;
            $scope.txttempaddressline1 = perm_address1;
            perm_address2 = $scope.txtperaddressline2;
            $scope.txttempaddressline2 = perm_address2;
            perm_country = $scope.txtpermcountry;
            $scope.txttempcountry = perm_country;
            perm_state = $scope.txtpermstate;
            $scope.txttermstate = perm_state;
            perm_city = $scope.txtpermcity;
            $scope.txttermcity = perm_city;
            perm_postalcode = $scope.txtpermpostalcode;
            $scope.txttermpostalcode = perm_postalcode;
        }
        var employee_gid = $location.search().employee_gid;

        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
      
        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeEditController';
        vm.calender06 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.open06 = true;
        };
        vm.formats = ['dd-MM-yyyy'];
        vm.format = vm.formats[0];
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        function activate() {
            
            lockUI();
            if ($scope.lstab == 'pending') {
                var url = 'api/ManageEmployee/EmployeePendingEditView';
            } 
            else {
                var url = 'api/ManageEmployee/EmployeeEditView';
            }
            var param = {
                employee_gid: employee_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
               
                $scope.employee_details = resp.data;
                $scope.txtentity = resp.data.company_name;
                $scope.txtbranch = resp.data.branch_gid;
                $scope.txtdepartment = resp.data.department_gid;
                $scope.txtsubfunction = resp.data.subfunction_gid;
                $scope.txtdesignation = resp.data.designation_gid;
                $scope.txtemployeeacess = resp.data.user_status;
                $scope.txtusercode = resp.data.user_code;
                $scope.cbobaselocation = resp.data.baselocation_gid;
                $scope.txtrole = resp.data.role_gid;
                $scope.txtreportingto = resp.data.employee_reportingto;
                $scope.txtuploadphoto = resp.data.employee_photo;
                $scope.txtfirstname = resp.data.user_firstname;
                $scope.txtlastname = resp.data.user_lastname;
                $scope.txtgender = resp.data.gender;
                $scope.txtuseremail = resp.data.employee_emailid;
                $scope.txtmobile = resp.data.employee_mobileno;
                $scope.txtperaddressline1 = resp.data.per_address1;
                $scope.txtperaddressline2 = resp.data.per_address2;
                $scope.txtpermcountry = resp.data.per_country_gid;
                $scope.txtpermstate = resp.data.per_state;
                $scope.txtpermcity = resp.data.per_city;
                $scope.txttermpostalcode = resp.data.temp_postal_code;
                $scope.txtpermpostalcode = resp.data.per_postal_code;
                $scope.txttempaddressline1 = resp.data.temp_address1;
                $scope.txttempaddressline2 = resp.data.temp_address2;
                $scope.txttempcountry = resp.data.temp_country_gid;
                $scope.txttermstate = resp.data.temp_state;
                $scope.txttermcity = resp.data.temp_city;
                $scope.cbomarital_status = resp.data.marital_status;
                $scope.cbomarital_status_gid = resp.data.marital_status_gid;
                $scope.cbobloodgroup_name = resp.data.bloodgroup_name;
                $scope.cbobloodgroup_gid = resp.data.bloodgroup_gid;
                $scope.cboemployee_joining_date = resp.data.joiningdate;
                $scope.txtpersonalphone_number = resp.data.personal_phone_no;
                $scope.txtpersonalemail = resp.data.personal_emailid;
            });

            var url = 'api/ManageEmployee/PopRole';
            SocketService.get(url).then(function (resp) {
                $scope.roleList = resp.data.rolemaster;
               
            }); 
            var url = 'api/ManageEmployee/PopReportingTo';
            SocketService.get(url).then(function (resp) {
                $scope.reportingtoList = resp.data.reportingto;
              
            });  

             var url = 'api/ManageEmployee/PopBranch';
            SocketService.get(url).then(function (resp) {
                $scope.branchList = resp.data.employee;
              
            });
            var url = 'api/ManageEmployee/PopDepartment';
            SocketService.get(url).then(function (resp) {
                $scope.departmentList = resp.data.employee;

              
            });

            var url = "api/ManageEmployee/PopSubfunction";
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.employee
            });

            var url = 'api/ManageEmployee/PopDesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designationList = resp.data.employee;
              
            }); 
            var url = 'api/ManageEmployee/PopCountry';
            SocketService.get(url).then(function (resp) {
                $scope.countryList = resp.data.country;
              
            });
            var url = 'api/SystemMaster/GetBaseLocationlistActive';
            SocketService.get(url).then(function (resp) {
                $scope.location_list = resp.data.location_list;

            });
            var url = 'api/MstApplication360/GetMaritalStatusActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_data = resp.data.application_list;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetBloodGroupActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bloodgroup_list = resp.data.master_list;
                unlockUI();
            });
            
            unlockUI();
           
                          
        }

        $scope.edit_cancel = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeRelievingSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
        };
        $scope.editemployee_update = function () {
        
        var maritalstatus_name = $('#cbomarital_status_gid :selected').text();
        var bloodgroup_name = $('#cbobloodgroup_gid :selected').text();

        var params = {
            employee_gid : employee_gid,            
            company_name : $scope.txtentity,
            branch_gid : $scope.txtbranch,
            department_gid: $scope.txtdepartment,
            subfunction_gid:$scope.txtsubfunction,
            designation_gid: $scope.txtdesignation,
            useraccess : $scope.txtemployeeacess,
            user_code : $scope.txtusercode,            
            role_gid: $scope.txtrole,
            employee_reportingto  : $scope.txtreportingto,
            employee_photo : $scope.txtuploadphoto,
            user_firstname : $scope.txtfirstname,
            user_lastname : $scope.txtlastname,
            gender : $scope.txtgender,
            employee_emailid : $scope.txtuseremail,
            employee_mobileno : $scope.txtmobile,
            per_address1: $scope.txtperaddressline1,
            per_address2 : $scope.txtperaddressline2,
            per_country_gid : $scope.txtpermcountry,
            per_state : $scope.txtpermstate,
            per_city : $scope.txtpermcity,
            per_postal_code : $scope.txtpermpostalcode,
            temp_address1 : $scope.txttempaddressline1,
            temp_address2 : $scope.txttempaddressline2,
            temp_country_gid : $scope.txttempcountry,
            temp_state : $scope.txttermstate,
            temp_city : $scope.txttermcity,
            temp_postal_code: $scope.txttermpostalcode,
            baselocation_gid: $scope.cbobaselocation,
            marital_status : maritalstatus_name,
            marital_status_gid : $scope.cbomarital_status_gid,
            bloodgroup_name : bloodgroup_name,
            bloodgroup_gid : $scope.cbobloodgroup_gid,
            joining_date : $scope.cboemployee_joining_date,
            personal_phone_no : $scope.txtpersonalphone_number,
            personal_emailid : $scope.txtpersonalemail

        }
        
        if ($scope.lstab == 'pending') {
            var url = 'api/ManageEmployee/EmployeePendingUpdate';
        } 
        else {
            var url = 'api/ManageEmployee/EmployeeUpdate';
        }
        lockUI();
        SocketService.post(url, params).then(function (resp) {
           
            if (resp.data.status == true) {
                 unlockUI();
                activate();
                if (lstab == 'pending') {
                    $location.url('app/SysMstEmployeePendingSummary');
                }
                else if (lstab == 'active') {
                    $state.go('app.SysMstEmployeeActiveUserSummary');
                }
                else if (lstab == 'inactive') {
                    $state.go('app.SysMstEmployeeInactiveSummary');
                }
                else if (lstab == 'relieving') {
                    $state.go('app.SysMstEmployeeInactiveSummary');
                }
                else {
                    $state.go('app.SysMstEmployeeSummary');   
                }
                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 4000
                });
            }
            

            else {
                Notify.alert(resp.data.message, {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 4000
                });
            }
            activate();
        });
                  
      
 }

 /* $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.txtuploadphoto);
                
                $scope.uploadfrm = frm;
                var url = 'api/IdasTrnSanctionDoc/conversation docupload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                   
    
                    $("#addupload").val('');
                    $("#editupload").val('');
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var params = {
                            employee_gid: $scope.employee_gid
                        }
                        var url = 'api/IdasTrnSanctionDoc/Getconversedoc';
    
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
    
                                $scope.txtuploadphoto = resp.data;
                            }
    
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')
    
                    }
    
                });
           
      
 } */

// $scope.postto_erp = function () {
//    var params = {
//        employee_gid: employee_gid
//    }
//    var url = 'api/SamAgroHBAPIConn/PostEmployeeToERP';
//    lockUI();
//    SocketService.getparams(url, params).then(function (resp) {
//        unlockUI();
//        if (resp.data.status == true) {
//            Notify.alert("Posted to ERP Successfully!", {
//                status: 'success',
//                pos: 'top-center',
//                timeout: 3000
//            });
//        }
//        else {
//            Notify.alert("Error Occured in posting to ERP..!", {
//                status: 'warning',
//                pos: 'top-center',
//                timeout: 3000
//            });
//        }


//    });
//}

}
    
})();
