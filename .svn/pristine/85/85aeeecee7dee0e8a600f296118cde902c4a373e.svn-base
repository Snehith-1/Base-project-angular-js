(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprUDCMakerEditChequeController', AgrMstSuprUDCMakerEditChequeController);

    AgrMstSuprUDCMakerEditChequeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprUDCMakerEditChequeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprUDCMakerEditChequeController';

        activate();
        function activate() {
            $scope.udcmanagement2cheque_gid = $location.search().lsudcmanagement2cheque_gid;
            $scope.udcmanagement_gid = $location.search().lsudcmanagement_gid;
            $scope.application_gid = $location.search().application_gid;
            var application_gid = $scope.application_gid;
            $scope.lspage = $location.search().lspage;
            var lspage = $scope.lspage;

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

            var params = {
                udcmanagement2cheque_gid: $scope.udcmanagement2cheque_gid
            }

            var url = 'api/AgrSuprUdcManagement/ChequeDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtstakeholder_name = resp.data.stakeholder_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtaccountholder_name = resp.data.accountholder_name;
                $scope.txtaccount_number = resp.data.account_number;
                $scope.txtbank_name = resp.data.bank_name;
                $scope.txtcheque_no = resp.data.cheque_no;
                $scope.txtifsc_code = resp.data.ifsc_code;
                $scope.txtmicr = resp.data.micr;
                $scope.txtbranch_address = resp.data.branch_address;
                $scope.txtbranch_name = resp.data.branch_name;
                $scope.txtcity = resp.data.city;
                $scope.txtdistrict = resp.data.district;
                $scope.txtstate = resp.data.state;
                $scope.cbomergedbanking_entity = resp.data.mergedbankingentity_gid;
                $scope.cbocheque_type = resp.data.cheque_type;

                $scope.txtdate_chequetype = resp.data.datechequetype;
                //$scope.txtdate_chequetype = Date.parse($scope.txtdate_chequetype);

                $scope.rbocts_enabled = resp.data.cts_enabled;

                $scope.txtdate_chequepresentation = resp.data.datechequepresentation;
                //$scope.txtdate_chequepresentation = Date.parse($scope.txtdate_chequepresentation);

                $scope.txtstatus_chequepresentation = resp.data.status_chequepresentation;

                $scope.txtdate_chequeclearance = resp.data.datechequeclearance;
                //$scope.txtdate_chequeclearance = Date.parse($scope.txtdate_chequeclearance);

                $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
                $scope.txtspecial_condition = resp.data.special_condition;
                $scope.txtgeneral_remarks = resp.data.general_remarks;
            });

            var url = 'api/AgrSuprUdcManagement/ChequeDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.chequedocument_list = resp.data.chequedocument_list;
            });

            var url = 'api/AgrSuprUdcManagement/GetDropDownUdc';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankname_list = resp.data.bankname_list;
                unlockUI();
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstSuprApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtapplication_no = resp.data.application_no;
                console.log(resp.data.application_no);
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                console.log(resp.data.customer_name);
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

        $scope.Back = function () {
            var application_gid = $scope.application_gid;
            var lspage = $scope.lspage;
            if (lspage == 'makerpending') {
                $location.url('app/AgrMstSuprUDCMakerSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerpending') {
                $location.url('app/AgrMstSuprChequeCheckerDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'approvalpending') {
                $location.url('app/AgrMstSuprChequeApprovalDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {
            }
        }

        $scope.update_cheque = function () {
            var application_gid = $scope.application_gid;
            var lspage = $scope.lspage;
            var mergedbankingentity_Name = $('#mergedbanking_entity :selected').text();

            var params = {
                mergedbankingentity_gid: $scope.cbomergedbanking_entity,
                mergedbankingentity_name: mergedbankingentity_Name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance,
                udcmanagement2cheque_gid: $scope.udcmanagement2cheque_gid
            }
            var url = 'api/AgrUdcManagement/UpdateChequeDetail';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == 'makerpending') {
                        $location.url('app/AgrMstUDCMakerSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == 'checkerpending') {
                        $location.url('app/AgrMstChequeCheckerDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == 'approvalpending') {
                        $location.url('app/AgrMstChequeApprovalDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else {
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }
    }
})();