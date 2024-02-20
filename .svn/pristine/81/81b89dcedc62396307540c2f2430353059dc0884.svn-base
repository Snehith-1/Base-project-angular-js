(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstcustomerAddChequeController', FndMstcustomerAddChequeController);

    FndMstcustomerAddChequeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function FndMstcustomerAddChequeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstcustomerAddChequeController';
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).customer_gid;        
        var customer_gid = cmnfunctionService.decryptURL($location.search().hash).customer_gid;
        activate();

        function activate() {
             vm.open3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened3 = true;
            };
            vm.open4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened4 = true;
            };
            vm.open5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            

            //var url = 'api/FndMstCustomerMasterAdd/GetUdcTempClear';
            //SocketService.get(url).then(function (resp) {
            //});
                   
            $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
            $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;

                //var params = {
                //    application_gid: $scope.customer_gid
                //}
                //var url = 'api/UdcManagement/GetStakeholders';
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.StakeholderList = resp.data.StakeholderList;
                //});

                //$scope.onChangeStakeholderName = function (stakeholder_gid) {
                //    var list = $scope.StakeholderList;

                //    for (var i = 0; i < list.length; i++) {
                //        if (list[i].stakeholder_gid == stakeholder_gid) {
                //            $scope.txtstakeholder_type = list[i].stakeholder_type;
                //            $scope.txtdesignation = list[i].designation;
                //            break;
                //        }
                //    }

                //}

                //var url = 'api/UdcManagement/GetDropDownUdc';
                //lockUI();
                //SocketService.get(url).then(function (resp) {
                //    $scope.bankname_list = resp.data.bankname_list;
                //    unlockUI();
                //});




        }
        //$scope.add_cheque = function () {
        //    $location.url('app/MstUDCMakerAddCheque?lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit');

        //}

        $scope.back = function(){
            $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

        }

        //$scope.add_cheque = function () {
        //    $state.go('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
        //}

        //$scope.add_cheque = function () {
        //    var params ={
               
                
        //        accountholder_name:$scope.txtaccountholder_name,
        //        account_number :$scope.txtaccount_number,
        //        bank_name :$scope.txtbank_name,
        //        cheque_no :$scope.txtcheque_no,
        //        ifsc_code :$scope.txtifsc_code,
        //        micr :$scope.txtmicr,
        //        branch_address :$scope.txtbranch_address,
        //        branch_name :$scope.txtbranch_name,
        //        city :$scope.txtcity,
        //        district :$scope.txtdistrict, 
        //        state :$scope.txtstate
               
        //    }
        //    var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {

        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
                    
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $location.url('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
        //    });

            
        //}

        var url = 'api/FndMstCustomerMasterAdd/GetDropDownUdc';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.bankname_list = resp.data.bankname_list;
            unlockUI();
        });

        $scope.delete_cheque = function (customer_gid) {
            lockUI();
            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }
        
    
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        //var phyPath = val1;
        //var relPath = phyPath.split("StoryboardAPI");
        //var relpath1 = relPath[1].replace("\\", "/");
        //var hosts = window.location.host;
        //var prefix = location.protocol + "//";
        //var str = prefix.concat(hosts, relpath1);
        //var link = document.createElement("a");
        //link.download = val2;
        //var uri = str;
        //link.href = uri;
        //link.click();
    }


    $scope.add_cheque = function () {

            var params = {              
                //accountholder_name: $scope.txtaccountholder_name,
                //account_number: $scope.txtaccount_number,
                //bank_name: $scope.txtbank_name,
                //cheque_no: $scope.txtcheque_no,
                //ifsc_code: $scope.txtifsc_code,
                //micr: $scope.txtmicr,
                //branch_address: $scope.txtbranch_address,
                //branch_name: $scope.txtbranch_name,
                //city: $scope.txtcity,
                //district: $scope.txtdistrict,
                //state: $scope.txtstate,


                //stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                //stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                //stakeholder_type: $scope.txtstakeholder_type,
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,            
                status_chequeclearance: $scope.txtstatus_chequeclearance
               
            }
            var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));
                    //$location.go('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();

              //  $scope.cboStakeholder = '',
              //$scope.txtstakeholder_type = '',
              //$scope.txtdesignation = '',
              $scope.txtaccountholder_name = '',
              $scope.txtaccount_number = '',
              $scope.txtbank_name = "",
              $scope.txtcheque_no = "",
              $scope.txtifsc_code = "",
              $scope.txtmicr = "",
              $scope.txtbranch_address = "",
              $scope.txtbranch_name = "",
              $scope.txtcity = "",
              $scope.txtdistrict = "",
              $scope.txtstate = "",
              $scope.cbomergedbanking_entity = "",
              $scope.txtspecial_condition = "",
              $scope.txtgeneral_remarks = "",
              $scope.rbocts_enabled = "",
              $scope.cbocheque_type = "",
              $scope.txtdate_chequetype = "",
              $scope.txtdate_chequepresentation = "",
              $scope.txtstatus_chequepresentation = "",
              $scope.txtdate_chequeclearance = "",
              $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;
                $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

            });
          

        }
    $scope.UploadDocument = function (val, val1, name) {
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
        frm.append('project_flag', "Default");
        $scope.uploadfrm = frm;
        if ($scope.uploadfrm != undefined) {
            var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
          
                if (resp.data.status == true){
                       var url = 'api/Kyc/ChequeOCR';
                       SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                           if (resp.data.statusCode == 101) {
                               $scope.txtaccountholder_name = resp.data.result.name[0];
                               $scope.txtaccount_number = resp.data.result.accNo;
                               $scope.txtbank_name = resp.data.result.bank;
                               $scope.txtcheque_no = resp.data.result.chequeNo;
                               $scope.txtifsc_code = resp.data.result.ifsc;
                               $scope.txtmicr = resp.data.result.micr;
                               $scope.txtbranch_address = resp.data.result.bankDetails.address;
                               $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                               $scope.txtcity = resp.data.result.bankDetails.city;
                               $scope.txtdistrict = resp.data.result.bankDetails.district;
                               $scope.txtstate = resp.data.result.bankDetails.state;
                           }
                           else {
                               Notify.alert('Error in fetching values from document..!', 'warning');
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $("#file").val('');
                $scope.uploadfrm = undefined;

                chequedocument_list();
                unlockUI();
            });
        }
        else {
            alert('Document is not Available..!');
            return;
        }
        
    }

    function chequedocument_list() {
        var url = 'api/FndMstCustomerMasterAdd/GetChequeDocumentList';
        SocketService.get(url).then(function (resp) {
            $scope.chequedocument_list = resp.data.chequedocument_list;
        });
    }


    $scope.delete_document = function (cheque2document_gid) {
        lockUI();
        var params = {
            cheque2document_gid: cheque2document_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentDelete';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.documentupload_list = resp.data.documentupload_list;
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            chequedocument_list();
            unlockUI();
        });
    }

        
}

    
})();
