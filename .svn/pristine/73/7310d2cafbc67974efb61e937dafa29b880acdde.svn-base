(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addLawfirmcontroller', addLawfirmcontroller);

    addLawfirmcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function addLawfirmcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addLawfirmcontroller';
        activate();
        function activate() {
            $scope.hidediv = true;
            $scope.showdiv = false;
           
            var url = 'api/lawFirm/lawfirm2lawyer';
            SocketService.get(url).then(function (resp) {
                $scope.lawfirm2lawyer_list = resp.data.lawfirm2lawyer_list;
            
            });
            var url = 'api/lawFirm/tempdelete';
            
            SocketService.get(url).then(function (resp) {
    
            });

        }

   
    $scope.lawfirmSubmit = function () {
            var lawyerdata = $('#lawyerdata :selected').text();
        var params = {
            firm_refno: $scope.txtfirm_refno,
            firm_name: $scope.txtfirm_name,
            contact_details: $scope.txtcontact_no,
            mail_address: $scope.txtemailaddress,
            fax_no: $scope.txtfaxno,
            firm_years: $scope.txtexperience,
            firm_address: $scope.txtaddress1,
            remarks: $scope.txtremarks,
            website: $scope.txtwebsite,
            lawfirm2lawyer_list: $scope.$parent.lawfirm2lawyer
           
        }

        var url = 'api/lawFirm/createLawfirm';
        lockUI()
      SocketService.post(url, params).then(function (resp) {
            if (resp.data.status == true) {
                unlockUI()
                activate();
                $state.go('app.lawfirmSummary');
                Notify.alert('Law Firm Added Successfully', 'success')
             

            }
            else {
                unlockUI();
                activate();
                $state.go('app.lawfirmSummary');
                Notify.alert('Error Occurred While Adding Law Firm', 'warning')
               
            }
            
        });
      
    }
    $scope.document_cancelclick = function (val, data) {
        var params = { document_gid: val };
        var url = 'api/lawFirm/documentdelete';
        lockUI()
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
                unlockUI()
              //  activate();
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
        var url = 'api/lawFirm/Uploadbankcertificate';
        lockUI();
        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
            $scope.filename_list = resp.data.UploadDocument;
            $("#addupload").val('');
            if (resp.data.status == true) {
                $scope.document_type='';
                unlockUI();
               
             Notify.alert('Document Uploaded Successfully', 'success')

            }
            else {
                unlockUI();
             Notify.alert('File Format Not Supported')

            }

        });

    }
    $scope.lawfirmback = function () {
        var url = 'api/lawFirm/tempdelete';
        lockUI()
        SocketService.get(url).then(function (resp) {
           
        });

        $state.go('app.lawfirmSummary');
    }

    $scope.addlawfirm2member = function () {
        var modalInstance = $modal.open({
            templateUrl: '/addmember.html',
            controller: ModalInstanceCtrl,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
           //Checking Enrolment No duplication
 $scope.enrolmentno_validation=function()
 {
     var params = {
         enrolment_no:$scope.txtenrolment_no
     }
     var url = 'api/lawFirm/GetLawfirmenrolment_validation';

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
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
    
            $scope.submitmember = function () {

                    var params = {
                        lawyer_name: $scope.txtmember_name,
                        mobile_no: $scope.txtmobileno,
                        email_address: $scope.txtemailaddress,
                        designation: $scope.txtdesignation,
                        enrolment_no: $scope.txtenrolment_no,
                    }
                    var url = 'api/lawFirm/postmembers';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            memberlist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                    });

                    $modalInstance.close('closed');
         

            }
        }
    }
    function memberlist() {
        var url = 'api/lawFirm/Gettempmember';
        
        SocketService.get(url).then(function (resp) {
            $scope.member_list = resp.data.member_list;
            
        });
    }
    $scope.memberdelete = function (val, data) {
        var params = { lawfirmmember_gid: val };
        console.log(params);
        var url = 'api/lawFirm/memberdelete';
        lockUI()
        SocketService.getparams(url, params).then(function (resp) {
            if (resp.data.status == true) {
                angular.forEach($scope.member_list, function (value, key) {
                    if (value.lawfirmmember_gid == val) {
                        $scope.member_list.splice(key, 1);
                    }
                });
                Notify.alert('Document Deleted Successfully', {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI()
                memberlist();
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

    $scope.taglawyer=function()
    {
        var modalInstance = $modal.open({
            templateUrl: '/taglawyer.html',
            controller: ModalInstanceCtrl,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
 
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            var url = 'api/lawFirm/lawfirm2lawyer';
            SocketService.get(url).then(function (resp) {
                $scope.lawfirm2lawyer_list = resp.data.lawfirm2lawyer_list;

            });
            $scope.submittag = function () {

                var params = {
                    lawyerregister_gid: $scope.cbolawfirm2lawyer.lawyerregister_gid,
                    designation: $scope.txtdesignation,
                   
                }
                console.log(params);
                var url = 'api/lawFirm/TagLawyer2lawfirm';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        memberlist();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }

                });

                $modalInstance.close('closed');


            }
        }
    }
    
    }
})();
