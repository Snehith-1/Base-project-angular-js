(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editRegisterlawyercontroller', editRegisterlawyercontroller);

    editRegisterlawyercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','cmnfunctionService'];

    function editRegisterlawyercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editRegisterlawyercontroller';
        activate();
        function activate() {
            vm.today = function () {
                vm.dt = new Date();
            };
            vm.today();
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            var url = 'api/registerlawyer/tempdelete';
            SocketService.get(url).then(function (resp) {
               
            });         
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.lawyerregister_gid = localStorage.getItem('lawyerregister_gid');

            var url = 'api/registerLawyer/Getlawyerdetails';
            var param = {
                lawyerregister_gid: $scope.lawyerregister_gid
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtlawyerref_noedit = resp.data.txtlawyerref_noedit;
                $scope.txtlawyernameedit = resp.data.txtlawyernameedit;
                $scope.txtdobedit = resp.data.dobedit;
                $scope.txtdobedit = Date.parse($scope.txtdobedit);
                $scope.genderedit = resp.data.genderedit;
             
                $scope.txtmobilenoedit = resp.data.txtmobilenoedit;
                $scope.txttelephonenoedit = resp.data.txttelephonenoedit;
                $scope.txtemailaddressedit = resp.data.txtemailaddressedit;
                $scope.txtqualificationedit = resp.data.txtqualificationedit;
                $scope.txtdateenrol_counciledit = resp.data.txtdateenrol_counciledit;
                $scope.txtdateenrol_counciledit = Date.parse($scope.txtdateenrol_counciledit);           
                $scope.txtpannoedit = resp.data.txtpannoedit;
                $scope.txtexperienceedit = resp.data.txtexperienceedit;
                $scope.txtplace_practiceedit = resp.data.txtplace_practiceedit;
                $scope.txtaddress1edit = resp.data.txtaddress1edit;
                $scope.txtaddress2edit = resp.data.txtaddress2edit;
                $scope.txtstateedit = resp.data.txtstateedit;
                $scope.txtcountryedit = resp.data.txtcountryedit;
                $scope.txtpostalcodeedit = resp.data.txtpostalcodeedit;
                $scope.filename_list = resp.data.UploadDocumentList;
                $scope.txtenrolment_no = resp.data.txtenrolment_no;
                $scope.txtaadhar_noedit = resp.data.aadhar_no;
                $scope.txtbank_nameedit = resp.data.bank_name;
                $scope.txtaccount_noedit = resp.data.account_no;
                $scope.txtifsc_codeedit = resp.data.ifsc_code;
                unlockUI();
                console.log(resp.data.txtlawyernameedit);
            });        
        }

        $scope.lawyerUpdate = function () {                      
            var params = {
                lawyerregister_gid: $scope.lawyerregister_gid,
                txtlawyerref_noedit: $scope.txtlawyerref_noedit,
                txtlawyernameedit: $scope.txtlawyernameedit,
                dob_edit: $scope.txtdobedit,
                genderedit: $scope.genderedit,
                txtfather_nameedit: $scope.txtfather_nameedit,
                txtmobilenoedit: $scope.txtmobilenoedit,
                txttelephonenoedit: $scope.txttelephonenoedit,
                txtemailaddressedit: $scope.txtemailaddressedit,
                txtqualificationedit: $scope.txtqualificationedit,
                txt_dateenrol_counciledit: $scope.txtdateenrol_counciledit,
                txtgstnoedit: $scope.txtgstnoedit,
                txtpannoedit: $scope.txtpannoedit,
                txtexperienceedit: $scope.txtexperienceedit,
                txtplace_practiceedit: $scope.txtplace_practiceedit,
                txtaddress1edit: $scope.txtaddress1edit,
                txtaddress2edit: $scope.txtaddress2edit,
                txtstateedit: $scope.txtstateedit,
                txtcountryedit: $scope.txtcountryedit,
                txtpostalcodeedit: $scope.txtpostalcodeedit,
                txtenrolment_no: $scope.txtenrolment_no,
                aadhar_no: $scope.txtaadhar_noedit,
                bank_name: $scope.txtbank_nameedit,
                account_no: $scope.txtaccount_noedit,
                ifsc_code: $scope.txtifsc_codeedit
            }
            console.log(params);
            var url = 'api/registerLawyer/lawyerUpdate';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.lawyerManagement');
                    Notify.alert('Lawyer Details Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating Lawyer Details')
                }              
            });
        }

        $scope.lawyerback = function () {
            var url = 'api/registerlawyer/tempdelete';
            lockUI()
            SocketService.get(url).then(function (resp) {
                
            });
            $state.go('app.lawyerManagement');
        }

        $scope.document_cancelclick = function (val, data) {
            var params = { document_gid: val };
            var url = 'api/registerLawyer/documentdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                  
                }
                else {
                    Notify.alert('Internal Error Occurred', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.photo = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photo");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "photo");
            $scope.uploadfrm = frm;
            localStorage.setItem($scope.uploadfrm, '$scope.uploadfrm');
           
            var url = 'api/registerLawyer/Uploadphoto';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
               // $scope.filename_list = resp.data.filename_list;

                $("#adduploadphoto").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.hidephotodiv = false;
                    $scope.showphotodiv = true;
                 
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported')
                }
            });

        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            $scope.lawyerregister_gid = localStorage.getItem('lawyerregister_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.document_type);
            frm.append('lawyerregister_gid',$scope.lawyerregister_gid);
            frm.append('project_flag', "Default");

            $scope.uploadfrm = frm;
            var url = 'api/registerLawyer/edituploadEnrollcertificate';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    $scope.showdiv = true;
                    $scope.hidediv = false;
                    Notify.alert('Document Uploaded Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
            });
        }

        //Checking Enrolment No duplication
        $scope.enrolmentno_validation=function()
        {
            var params = {
                enrolment_no:$scope.txtenrolment_no
            }
            var url = 'api/registerlawyer/Getenrolment_validation';

            SocketService.getparams(url,params).then(function (resp) {
                console.log(resp.data.enrolment_no);
                if (resp.data.enrolment_no == 'Y') {
                    $scope.validationMsg = true;
                }
                else {
                    $scope.validationMsg = false;
                }
            });
        }
    }
})();
