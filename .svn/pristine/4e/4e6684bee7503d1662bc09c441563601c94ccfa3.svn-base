(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2PreparationView', tier2PreparationView);

    tier2PreparationView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier2PreparationView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2PreparationView';

        activate();

        function activate() {
            lockUI();
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier2Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
            });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.cbomonth = resp.data.tier2_month;
                $scope.monthname = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.cboemployeegid = resp.data.headRMD_gid;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.zonalmapping_gid = resp.data.zonalmapping_gid;
                if (resp.data.tier2approvallog == null) {
                    //$scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }

                if ($scope.tier2_approval_status == "Pending" || $scope.tier2_approval_status == "Rejected") {
                    $scope.edittier2dtl = true;
                    $scope.viewtier2dtl = false;
                }
                else {

                    $scope.edittier2dtl = false;
                    $scope.viewtier2dtl = true;
                }
                unlockUI();
            });

        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }


        $scope.riskcodehistory = function (allocationdtl_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodeHistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
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

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, tier2preparation_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
                $scope.ok = function () {
                    $scope.txtcode_changereason = "";
                    $modalInstance.close('closed');

                };

                $scope.cboriskcodechange = function (cboriskcode) {
                    if (tier2_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.txtcode_changereason = "";
                        $scope.codechangereasonshow = true;
                       
                    }
                }
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });

                $scope.riskcodechangecancel = function (tmptier2_codechange) {
                    var params = {
                        tmptier2_codechange: tmptier2_codechange
                    }
                    lockUI();
                    var url = "api/TierMeeting/GetTier2ColorDelete"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtcode_changereason = "";
                            unlockUI();
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTierColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;

                            });
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

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tierallocation_gid: tierallocation_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier2preparation_gid: tier2preparation_gid,
                        tier_code: $scope.cboriskcode,
                        tiercode_changereason: $scope.txtcode_changereason,
                        tier3_flag: "N"
                    }
                    
                    lockUI();
                    var url = "api/TierMeeting/PostTierColorUpdate"
                    SocketService.post(url, params).then(function (resp) {

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

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'Y'
            }

            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
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
                frm.append('tier2preparartion_gid', localStorage.getItem('tier2preparation_gid'));
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier2TrnUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier2document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = '';
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

        $scope.uploadcancel = function (tier2document_gid) {
            var tier2upload = {
                tier2document_gid: tier2document_gid,
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2TrnUploadCancel';
            SocketService.getparams(url, tier2upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.tier2document;
            });
        }
        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'Y',
                zonalmapping_gid: $scope.zonalmapping_gid
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;

            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.updatetier2 = function () {
            lockUI();
            var vertical_name = $('#cboverticalname :selected').text();
            var headRMD_name = $('#cboemployeename :selected').text();
            var tier2dtl = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                tier2_month: $scope.cbomonth,
                vertical_gid: $scope.cbovertical_gid,
                vertical: vertical_name,
                headRMD_gid: $scope.cboemployeegid,
                headRMD_name: headRMD_name,
                tier2_remarks: $scope.txttier2_remarks,
            }

            var url = "api/TierMeeting/PostUpdateTier2";
            SocketService.post(url, tier2dtl).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2Preparation');

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
