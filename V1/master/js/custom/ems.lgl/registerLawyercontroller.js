(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addLawyerController', addLawyerController);

    addLawyerController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','DownloaddocumentService','cmnfunctionService'];

    function addLawyerController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'addLawyerController';
        activate();
        function activate() {
         
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;
            $scope.validationMsg = false;
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/registerlawyer/tempdelete';
          
            SocketService.get(url).then(function (resp) {

               
            });

        }
        //Back Button Event
        $scope.lawyerback = function () {
            var url = 'api/registerlawyer/tempdelete';
          
            SocketService.get(url).then(function (resp) {
              
            });

            $state.go('app.lawyerManagement');
        }
        // Register lawyer
        $scope.lawyerSubmit = function () {
            localStorage.getItem($scope.uploadfrm);
            var params = {
                lawyerref_no: $scope.txtlawyerref_no,
                lawyer_name: $scope.txtlawyername,
                dob: $scope.dob,
                gender: $scope.gender,
                mobile_no: $scope.txtmobileno,
                telephone_no: $scope.txttelephoneno,
                email_address: $scope.txtemailaddress,
                educational_qualification: $scope.txtqualification,
                date_enrolment: $scope.txtdateenrol_council,
                pan_no: $scope.txtpanno,
                experience: $scope.txtexperience,
                place_practice: $scope.txtplace_practice,
                address_line1: $scope.txtaddress1,
                address_line2: $scope.txtaddress2,
                state: $scope.txtstate,
                country: $scope.txtcountry,
                postal_code: $scope.txtpostalcode,
                enrolment_no: $scope.txtenrolment_no,
                aadhar_no: $scope.txtaadhar_no,
                bank_name: $scope.txtbank_name,
                account_no: $scope.txtaccount_no,
                ifsc_code: $scope.txtifsc_code,
            }
        
            var url = 'api/registerlawyer/registerlawyer';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.lawyerManagement');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }
        //Upload Empanelment Certificate
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
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.document_type);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
    
            var url = 'api/registerlawyer/UploadEnrollcertificate';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;
          
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    $scope.showdiv = true;
                    $scope.hidediv = false;
                    Notify.alert(resp.data.message,'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
            });
        }
        //Document Delete
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
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        //upload Lawyer photo
        $scope.photo = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name,"photo");

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
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            localStorage.setItem($scope.uploadfrm, '$scope.uploadfrm');

            var url = 'api/registerLawyer/Uploadphoto';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;
                $("#adduploadphoto").val('');
                var url = 'api/registerLawyer/tempdocument';
                SocketService.get(url).then(function (resp) {
                    $scope.filename_list = resp.data.UploadDocumentList;
 
                });
                if (resp.data.status == true) {
                  
                    $scope.hidephotodiv = false;
                    $scope.showphotodiv = true;
                  
                }
                else {
                    
                  
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
