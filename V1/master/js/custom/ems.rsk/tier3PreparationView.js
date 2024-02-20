(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3PreparationView', tier3PreparationView);

    tier3PreparationView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function tier3PreparationView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3PreparationView';

        activate();

        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            lockUI();

            var url = 'api/TierMeeting/GetTier3Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
            });

            var url = 'api/Vertical/Vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var params = {
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier3ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.mlrc_date = resp.data.MLRC_date;
                $scope.txtMLRC_date = resp.data.MLRC_Date;
                $scope.cbomonth = resp.data.tier3_month;
                $scope.monthname = resp.data.tier3_monthname;
                $scope.txttier3_followup = resp.data.follow_up;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier3document;
                $scope.completed_date = resp.data.completed_date;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.tierallocationdtl = resp.data.tierallocationdtl

                if ($scope.completed_flag == "N") {
                    $scope.edittier3dtl = true;
                    $scope.viewtier3dtl = false;
                }
                else {

                    $scope.edittier3dtl = false;
                    $scope.viewtier3dtl = true;
                }
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, customer_name, customer_urn, tier3_code) {
            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier3_code;
                $scope.txtcode_changereason = "";
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };
                $scope.cboriskcodechange = function (cboriskcode) {
                    if (tier3_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.codechangereasonshow = true;
                        $scope.txtcode_changereason = "";
                    }
                }

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tierallocation_gid: tierallocation_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier_code: $scope.cboriskcode,
                        tiercode_changereason: $scope.txtcode_changereason,
                        tier3_flag: "Y"
                    }
                    lockUI();
                    var url = "api/TierMeeting/PostTierColorUpdate"
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            $modalInstance.close('closed');
                            activate();
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }


        $scope.riskcodehistory = function (allocationdtl_gid, customer_name, customer_urn, tier3_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodeHistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier3_code;
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });
            }
        }

        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'N',
                zonalmapping_gid: ""
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;

            });
        }

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                }
                // var item = {
                //     name: val[0].name,
                //     file: val[0]
                // };
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
                //         status: 'danger',
                //         pos: 'top-center',
                //         timeout: 3000
                //     });
                //     return false;
                // }
                // var frm = new FormData();
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('tier3preparation_gid', localStorage.getItem('tier3preparation_gid'));
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTrnTier3Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier3document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }

        $scope.uploadcancel = function (tier3document_gid) {
            var tier3upload = {
                tier3document_gid: tier3document_gid,
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier3TrnUploadCancel';
            SocketService.getparams(url, tier3upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.tier3document;
            });
        }

        $scope.updatetier3 = function () {
            lockUI();
            var vertical_name = $('#cboverticalname :selected').text();
            var tier3dtl = {
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid'),
                tier3_month: $scope.cbomonth,
                vertical_gid: $scope.cbovertical_gid,
                vertical: vertical_name,
                MLRC_date: $scope.txtMLRC_date,
                follow_up: $scope.txttier3_followup,
            }
            console.log(tier3dtl);
            var url = "api/TierMeeting/PostUpdateTier3";
            SocketService.post(url, tier3dtl).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }
    }
})();
