(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeAddController', SysMstEmployeeAddController);

        SysMstEmployeeAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

    function SysMstEmployeeAddController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        
        var vm = this;
        vm.title = 'SysMstEmployeeAddController';
        $scope.employeeaccess_no = false;
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;


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
       
       
        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.employeeaccess_no = true;

            }
            else if (param == "hide") {
                $scope.employeeaccess_no = false;
                
            }
            
        }
        activate();
        function activate() {
            lockUI();
           $scope.entitytext =false;
           $scope.entitydrop = false;
           $scope.txtpermcountry = 'CN06070099';
           $scope.txttempcountry = 'CN06070099';
            var url = 'api/ManageEmployee/EntityName';
            SocketService.get(url).then(function (resp) {
                $scope.txtentity = resp.data.entity_name;
                $scope.entity_flag = resp.data.entity_flag;
                if( resp.data.entity_flag =="Y"){
                    $scope.entitytext =true;
                    $scope.entitydrop = false;
                }
                else{
                    $scope.entitytext =false;
                    $scope.entitydrop = true;
                }
             }); 

            var url = 'api/ManageEmployee/PopEntityActive';
            SocketService.get(url).then(function (resp) {
             
                $scope.entityList = resp.data.entity;
                              
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
            var url = 'api/ManageEmployee/PopDesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designationList = resp.data.employee;
              
            }); 
            var url = 'api/ManageEmployee/PopCountry';
            SocketService.get(url).then(function (resp) {
                $scope.countryList = resp.data.country;
              
            });
            var url = "api/ManageEmployee/PopSubfunction";
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.employee
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
            unlockUI();
                   
        }

        $scope.add_cancel = function () {
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
           // $state.go('app.SysMstEmployeeSummary');   
            };

  /* Employee Add */

        $scope.addemployee_submit = function () {
            if($scope.txtgender == undefined){
                $scope.txtgender = 'Male' 
                console.log($scope.txtgender);
            }
            else
            {
                console.log($scope.txtgender);
            }
            if($scope.cbomarital_status == undefined){
                var maritalstatus_name = '';
                var maritalstatus_gid = '';
            }
            else{
                var maritalstatus_name = $scope.cbomarital_status.maritalstatus_name;
                var maritalstatus_gid = $scope.cbomarital_status.maritalstatus_gid;   
            }
            if($scope.cbobloodgroup_name == undefined){
                var bloodgroup_name = '';
                var bloodgroup_gid = '';
            }
            else{
                var bloodgroup_name = $scope.cbobloodgroup_name.bloodgroup_name;
                var bloodgroup_gid = $scope.cbobloodgroup_name.bloodgroup_gid;
            }
            if ($scope.txtemployeeacess == undefined) {
                $scope.txtemployeeacess = 'Y';
            }

            if ($scope.txtemployeeacess == "Y")
            {
                if ($scope.txtuserpassword == '' || $scope.txtuserpassword == null || $scope.txtuserpassword == undefined) {
                    alert('Enter User Password', 'warning');
                }
                else if ($scope.txtconfirmpassword == '' || $scope.txtconfirmpassword == null || $scope.txtconfirmpassword == undefined) {
                    alert('Enter Confirm Password', 'warning');
                }
                else {
                   
                    var params = {
                        company_name: $scope.txtentity,
                        entity_gid: $scope.txtentitydrop,
                        branch_gid: $scope.txtbranch.branch_gid,
                        department_gid: $scope.txtdepartment.department_gid,
                        designation_gid: $scope.txtdesignation.designation_gid,
                        useraccess: $scope.txtemployeeacess,
                        user_code: $scope.txtusercode,
                        user_password: $scope.txtuserpassword,
                        user_password: $scope.txtconfirmpassword,
                        role_gid: $scope.txtrole.role_gid,
                        employee_reportingto: $scope.txtreportingto.employee_gid,
                        employee_photo: $scope.txtuploadphoto,
                        user_firstname: $scope.txtfirstname,
                        user_lastname: $scope.txtlastname,
                        gender: $scope.txtgender,
                        employee_emailid: $scope.txtuseremail,
                        employee_mobileno: $scope.txtmobile,
                        per_address1: $scope.txtperaddressline1,
                        per_address2: $scope.txtperaddressline2,
                        per_country_gid: $scope.txtpermcountry.country_gid,
                        per_state: $scope.txtpermstate,
                        per_city: $scope.txtpermcity,
                        per_postal_code: $scope.txtpermpostalcode,
                        temp_address1: $scope.txttempaddressline1,
                        temp_address2: $scope.txttempaddressline2,
                        temp_country_gid: $scope.txttempcountry.country_gid,
                        temp_state: $scope.txttermstate,
                        temp_city: $scope.txttermcity,
                        temp_postal_code: $scope.txttermpostalcode,
                        baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                        marital_status : maritalstatus_name,
                        marital_status_gid :maritalstatus_gid,
                        bloodgroup_name : bloodgroup_name,
                        bloodgroup_gid : bloodgroup_gid,
                        joining_date : $scope.cboemployee_joining_date,
                        personal_phone_no : $scope.txtpersonalphone_number,
                        personal_emailid: $scope.txtpersonalemail,
                        subfunction_gid: $scope.txtsubfunction

                    }

                    var url = 'api/ManageEmployee/EmployeeAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 4000
                            });
                            activate();
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                            unlockUI();
                            //activate();
                            //if (lstab == 'pending') {
                            //    $location.url('app/SysMstEmployeePendingSummary');
                            //}
                            //else if (lstab == 'active') {
                            //    $state.go('app.SysMstEmployeeActiveUserSummary');
                            //}
                            //else if (lstab == 'inactive') {
                            //    $state.go('app.SysMstEmployeeInactiveSummary');
                            //}
                            //else if (lstab == 'relieving') {
                            //    $state.go('app.SysMstEmployeeRelievingSummary');
                            //}
                            //else {
                            //    $state.go('app.SysMstEmployeeSummary');
                            //}
                            
                        }
                    });
                }
            }

            else 
            {
                var params = {
                    company_name: $scope.txtentity,
                    entity_gid: $scope.txtentitydrop,
                    branch_gid: $scope.txtbranch.branch_gid,
                    department_gid: $scope.txtdepartment.department_gid,
                    designation_gid: $scope.txtdesignation.designation_gid,
                    useraccess: $scope.txtemployeeacess,
                    user_code: $scope.txtusercode,
                    role_gid: $scope.txtrole.role_gid,
                    employee_reportingto: $scope.txtreportingto.employee_gid,
                    employee_photo: $scope.txtuploadphoto,
                    user_firstname: $scope.txtfirstname,
                    user_lastname: $scope.txtlastname,
                    gender: $scope.txtgender,
                    employee_emailid: $scope.txtuseremail,
                    employee_mobileno: $scope.txtmobile,
                    per_address1: $scope.txtperaddressline1,
                    per_address2: $scope.txtperaddressline2,
                    per_country_gid: $scope.txtpermcountry.country_gid,
                    per_state: $scope.txtpermstate,
                    per_city: $scope.txtpermcity,
                    per_postal_code: $scope.txtpermpostalcode,
                    temp_address1: $scope.txttempaddressline1,
                    temp_address2: $scope.txttempaddressline2,
                    temp_country_gid: $scope.txttempcountry.country_gid,
                    temp_state: $scope.txttermstate,
                    temp_city: $scope.txttermcity,
                    temp_postal_code: $scope.txttermpostalcode,
                    baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                    subfunction_gid: $scope.txtsubfunction.subfunction_gid
                }

                var url = 'api/ManageEmployee/EmployeeAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        $state.go('app.SysMstEmployeeSummary');
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        //activate();
                        //$state.go('app.SysMstEmployeeSummary');
                        unlockUI();
                    }
                });
            }
        }  

        $scope.user_code_check = function (user_gid) {
            var params = {
                user_gid : user_gid
            }
            var url = 'api/ManageEmployee/UserCodeCheck';
            SocketService.getparams(url, params).then(function (resp) {
            $scope.user_message = resp.data.message;
            if( $scope.user_message == "User Code Already in Use")
            { $scope.message_color = 1}
            else
            { $scope.message_color = 2}
            });
        };

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
    } 
})();
