(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditorMasterChequeEditController', AgrMstCreditorMasterChequeEditController);

    AgrMstCreditorMasterChequeEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstCreditorMasterChequeEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditorMasterChequeEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.creditor_gid = searchObject.creditor_gid;

        $scope.creditor2cheque_gid = searchObject.creditor2cheque_gid;

        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        //lockUI();
        function activate() {

            lockUI();

            vm.calender11 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open11 = true;
            };
            // Calender Popup... //

            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open12 = true;
            };

            vm.calender13 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open13 = true;
            };

            vm.calender14 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open14 = true;
            };

            vm.calender15 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open15 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                creditor_gid: $scope.creditor_gid
            };

            var url = 'api/UdcManagement/GetDropDownUdc';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankname_list = resp.data.bankname_list;
                unlockUI();
            });

            var params = {
                creditor_gid: $scope.creditor_gid
            }

            var url = 'api/AgrMstCreditorMaster/EditCreditorDetails';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtapplicant_name = resp.data.Applicant_name;
                $scope.txtcreditor_code = resp.data.creditorref_no;

                unlockUI();
            });

            var params = {
                creditor2cheque_gid: $scope.creditor2cheque_gid
            }
            var url = 'api/AgrMstCreditorMaster/ChequeDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {

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
                $scope.rdbprimarystatus = resp.data.primary_status;

                //$scope.txtdate_chequetype = resp.data.datechequetype;

                if (resp.data.datechequetype == "0001-01-01T00:00:00") {

                    $scope.txtdate_chequetype = '';
                }

                else {
                    $scope.txtdate_chequetype = resp.data.datechequetype;

                }
                //$scope.txtdate_chequetype = Date.parse($scope.txtdate_chequetype);

                $scope.rbocts_enabled = resp.data.cts_enabled;

                if (resp.data.datechequepresentation == "0001-01-01T00:00:00") {

                    $scope.txtdate_chequepresentation = '';
                }

                else {

                    $scope.txtdate_chequepresentation = resp.data.datechequepresentation;

                }
                //$scope.txtdate_chequepresentation = Date.parse($scope.txtdate_chequepresentation);

                $scope.txtstatus_chequepresentation == resp.data.status_chequepresentation;
                if (resp.data.datechequeclearance == "0001-01-01T00:00:00") {

                    $scope.txtdate_chequeclearance = '';
                }
                else {
                    $scope.txtdate_chequeclearance = resp.data.datechequeclearance;

                }

                
                //$scope.txtdate_chequeclearance = Date.parse($scope.txtdate_chequeclearance);

                $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
                $scope.txtspecial_condition = resp.data.special_condition;
                $scope.txtgeneral_remarks = resp.data.general_remarks;
            });


        }

        $scope.update_cheque = function () {

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
                primary_status: $scope.rdbprimarystatus,
                creditor2cheque_gid: $scope.creditor2cheque_gid
            }
            var url = 'api/AgrMstCreditorMaster/UpdateChequeDetail';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                //    $location.url('app/AgrMstCreditorMasterEdit?creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid);
                    if (lspage == "Approved") {
                        $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid + '&lspage=Approved'));
                    }

                    else {

                        $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid));

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

        $scope.Back = function () {

            if (lspage == "Approved") {
                $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid + '&lspage=Approved'));
            }

            else {

                $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid));

            }

        //    $location.url('app/AgrMstCreditorMasterEdit?creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + $scope.creditor2cheque_gid);
        }

    }

})();