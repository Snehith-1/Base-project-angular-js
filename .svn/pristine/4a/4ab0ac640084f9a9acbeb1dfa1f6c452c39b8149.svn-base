(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplGroupdtlViewController', MstApplGroupdtlViewController);

    MstApplGroupdtlViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstApplGroupdtlViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplGroupdtlViewController';
        var group_gid = localStorage.getItem('group_gid');
        activate();

        function activate() {
            var params =
               {
                   group_gid: group_gid
               }

            lockUI();
            var url = 'api/MstApplicationEdit/EditGroup';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtdate_formation = resp.data.date_of_formation;
                $scope.txtgroup_type = resp.data.group_type;
                $scope.txtmember_count = resp.data.groupmember_count;
                $scope.txtmember_URN = resp.data.group_urn;
                $scope.groupurn_status = resp.data.groupurn_status;
                $scope.internalrating_gid = resp.data.internalrating_gid,
                $scope.txtinternalrating_name = resp.data.internalrating_name,
                $scope.txtmale_count = resp.data.male_count,
                $scope.txtfemale_count = resp.data.female_count
            })

            var params =
            {
                group_gid: group_gid
            }

            lockUI();
            var url = 'api/MstApplicationEdit/GroupAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.memberaddress_list = resp.data.mstaddress_list;
            });

            var params =
            {
                group_gid: group_gid
            }

            lockUI();
            var url = 'api/MstApplicationEdit/GroupBankList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.memberbank_list = resp.data.mstbank_list;
            });

            var params =
            {
                group_gid: group_gid
            }

            lockUI();
            var url = 'api/MstApplicationEdit/GroupDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
            });

            var params = {
                group_gid: group_gid
            }
            var url = 'api/MstApplicationEdit/GetGroupEquipmentHoldingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
            });

            var params = {
                group_gid: group_gid
            }
            var url = 'api/MstApplicationEdit/GetGroupLivestockList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
            });

            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            $scope.group_docs = function (val1, val2) {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
        }
        $scope.equipment_View = function (group2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    group2equipment_gid: group2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetGroupEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.livestock_View = function (group2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    group2livestock_gid: group2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetGroupLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.close = function () {
            window.close();
        }

       
        $scope.downloadallmember = function () {
            for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
            }
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

    }
})();
