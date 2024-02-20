(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCcCommitteeGroupViewController', AgrTrnCcCommitteeGroupViewController);

    AgrTrnCcCommitteeGroupViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AgrTrnCcCommitteeGroupViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCcCommitteeGroupViewController';
        var group_gid = localStorage.getItem('group_gid');
        var application_gid = localStorage.getItem('application_gid');

        lockUI();
        activate();

        function activate() {

            var params = {
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/EditPSLDataFlagging';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtstartloan_date = resp.data.startupasofloansanction_date;
                $scope.txtoccupation = resp.data.occupation;
                $scope.txtline_activity = resp.data.lineofactivity;
                $scope.txtbsr_code = resp.data.bsrcode;
                $scope.txtpsl_category = resp.data.pslcategory;
                $scope.txtweaker_section = resp.data.weakersection;
                $scope.txtpsl_purpose = resp.data.pslpurpose;
                $scope.txttotal_financialinstitution = resp.data.totalsanction_financialinstitution;
                $scope.txtpsl_sanctionlimit = resp.data.pslsanction_limit;
                $scope.txtnature_entity = resp.data.natureofentity;
                $scope.txtmarketing_activities = resp.data.indulgeinmarketing_activity;
                $scope.txtplant_machienery = resp.data.plantandmachineryinvestment;
                $scope.txtturnover = resp.data.turnover;
                $scope.txtmsme_classification = resp.data.msmeclassification;
                $scope.txtloansanction_date = resp.data.loansanction_date;
                $scope.txtentityincorporate_date = resp.data.entityincorporation_date;
                $scope.txthq_city = resp.data.hq_metropolitancity;
                $scope.txtclient_details = resp.data.clientdtl_name;
            });

            var params = {
                credit_gid: group_gid
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCrediBankAccList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: group_gid
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ExistingBankacctDtl_list = resp.data.cuwexistingbankfacility_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RepaymentDtl_list = resp.data.cuwrepaymenttrack_list;
            });

            var params = {
                credit_gid: group_gid
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            var params = {
                group_gid: group_gid
            }

            lockUI();
            var url = 'api/AgrMstApplicationEdit/EditGroup';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtdate_formation = resp.data.date_of_formation;
                $scope.txtgroup_type = resp.data.group_type;
                $scope.txtmember_count = resp.data.groupmember_count;
                $scope.txtmember_URN = resp.data.group_urn;
                $scope.groupurn_status = resp.data.groupurn_status;
            });

            var params = {
                group_gid: group_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationEdit/GroupAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.memberaddress_list = resp.data.mstaddress_list;
            });

            var params = {
                group_gid: group_gid
            }

            lockUI();
            var url = 'api/AgrMstApplicationEdit/GroupBankList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.memberbank_list = resp.data.mstbank_list;
            });

            var params = {
                group_gid: group_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationEdit/GroupDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
            });

        }

        $scope.close = function () {
            window.close();
        }

        $scope.group_docs = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
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

        $scope.uploadeddoc_bankacctdtl = function (creditbankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Bankacctdocuments.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditbankdtl_gid: creditbankdtl_gid
                  }
                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditBankDocumentUpload';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chequeleaf_list = resp.data.credituploaddocument_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    // var phyPath = val1;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // link.download = val2;
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
                    }
                }

            }

        }

        $scope.repayment_remarks = function (creditrepaymentdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Remarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditrepaymentdtl_gid: creditrepaymentdtl_gid
                  }
                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditRepaymentDtlRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.repayment_remarks = resp.data.Repayment_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.Existingbank_remarks = function (existingbankfacility_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ExistingRemarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    existingbankfacility_gid: existingbankfacility_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditExistingBankDtlRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.existing_remarks = resp.data.Existingbank_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
            }
        }
        $scope.downloadall_8 = function () {
            for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
            }
        }

    }
})();
