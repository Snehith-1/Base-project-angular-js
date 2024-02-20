(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstUDCMakerAddChequeController', MstUDCMakerAddChequeController);

    MstUDCMakerAddChequeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstUDCMakerAddChequeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstUDCMakerAddChequeController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/UdcManagement/GetUdcTempClear';
            SocketService.get(url).then(function (resp) {
            });
                   
            $scope.customer_gid = $location.search().lscustomer_gid;
            $scope.udcmanagement_gid = $location.search().lsudcmanagement_gid;

                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/UdcManagement/GetStakeholders';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.StakeholderList = resp.data.StakeholderList;
                });

                $scope.onChangeStakeholderName = function (stakeholder_gid) {
                    var list = $scope.StakeholderList;

                    for (var i = 0; i < list.length; i++) {
                        if (list[i].stakeholder_gid == stakeholder_gid) {
                            $scope.txtstakeholder_type = list[i].stakeholder_type;
                            $scope.txtdesignation = list[i].designation;
                            break;
                        }
                    }

                }

                var url = 'api/UdcManagement/GetDropDownUdc';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.bankname_list = resp.data.bankname_list;
                    unlockUI();
                });




        }

        $scope.back = function(){
            $location.url('app/MstUDCMakerEdit?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&lstab=edit');

        }
        $scope.add_cheque = function () {
            var lspage = $scope.lspage;
            var params ={
                stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                stakeholder_type: $scope.txtstakeholder_type,
                designation: $scope.txtdesignation,
                accountholder_name:$scope.txtaccountholder_name,
                account_number :$scope.txtaccount_number,
                bank_name :$scope.txtbank_name,
                cheque_no :$scope.txtcheque_no,
                ifsc_code :$scope.txtifsc_code,
                micr :$scope.txtmicr,
                branch_address :$scope.txtbranch_address,
                branch_name :$scope.txtbranch_name,
                city :$scope.txtcity,
                district :$scope.txtdistrict, 
                state :$scope.txtstate, 
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks :$scope.txtgeneral_remarks, 
                cts_enabled :$scope.rbocts_enabled, 
                cheque_type :$scope.cbocheque_type, 
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance
            }
            var url = 'api/UdcManagement/PostChequeDetail';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

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
                if (lspage == 'makerpending') {
                    $location.url('app/MstUDCMakerSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                }
                else if (lspage == 'checkerpending') {
                    $location.url('app/MstChequeCheckerDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                }
                else if (lspage == 'approvalpending') {
                    $location.url('app/MstChequeApprovalDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                }
                else {
                }
            });

            
        }

        $scope.delete_cheque = function (udcmanagement2cheque_gid) {
            lockUI();
            var params = {
                udcmanagement2cheque_gid: udcmanagement2cheque_gid
            }
            var url = 'api/UdcManagement/DeleteChequeDetail';
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
        var phyPath = val1;
        var relPath = phyPath.split("StoryboardAPI");
        var relpath1 = relPath[1].replace("\\", "/");
        var hosts = window.location.host;
        var prefix = location.protocol + "//";
        var str = prefix.concat(hosts, relpath1);
        var link = document.createElement("a");
        link.download = val2;
        var uri = str;
        link.href = uri;
        link.click();
    }

    $scope.UploadDocument = function (val, val1, name) {
        var item = {
            name: val[0].name,
            file: val[0]
        };
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
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
        $scope.uploadfrm = frm;
        if ($scope.uploadfrm != undefined) {
            var url = 'api/UdcManagement/ChequeDocumentUpload';
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
        var url = 'api/UdcManagement/GetChequeDocumentList';
        SocketService.get(url).then(function (resp) {
            $scope.chequedocument_list = resp.data.chequedocument_list;
        });
    }


    $scope.delete_document = function (cheque2document_gid) {
        lockUI();
        var params = {
            cheque2document_gid: cheque2document_gid
        }
        var url = 'api/UdcManagement/ChequeDocumentDelete';
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
