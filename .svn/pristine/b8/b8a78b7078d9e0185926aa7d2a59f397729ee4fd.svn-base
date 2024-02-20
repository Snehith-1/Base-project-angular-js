(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Createcontroller', tier2Createcontroller);

    tier2Createcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier2Createcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Createcontroller';

        activate();

        function activate() {
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier2Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.zonalmapping_gid = resp.data.zonalmapping_gid;
                unlockUI();
            });

        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid',allocationdtl_gid);
            localStorage.setItem('tier1format_gid',tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
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

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'Y'
            }
            lockUI();
            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
                unlockUI();
            });
        }

        $scope.riskcodechange = function (allocationdtl_gid, tier1format_gid, customer_name, customer_urn, tier1_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.txtcode_changereason = "";
                $scope.cboriskcode = tier1_code;
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTier2ColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;
                    $scope.tmptier_stage = resp.data.tier_stage;
                    $scope.tmptier_code = resp.data.tier_code;
                    $scope.tmptiercode_changereason = resp.data.tiercode_changereason;
                    $scope.tmptier2_codechange = resp.data.tmptier2_codechange
                    if (resp.data.tmptier2_codechange == null || resp.data.tmptier2_codechange == undefined) {
                        $scope.tmpcodevisible = true;
                        $scope.tmpdata = false;
                    }
                    else {
                        $scope.tmpcodevisible = false;
                        $scope.tmpdata = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cboriskcodechange = function (cboriskcode) {
                    
                    if (tier1_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.codechangereasonshow = true;
                        $scope.txtcode_changereason = "";
                    }
                }

                $scope.riskcodechangecancel = function (tmptier2_codechange) {
                    var params = {
                        tmptier2_codechange: tmptier2_codechange
                    }
                    lockUI();
                    var url = "api/TierMeeting/GetTier2ColorDelete"
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $scope.tmpdata = false;
                            $scope.tmpcodevisible = true;
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTier2ColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;
                                $scope.cboriskcode = tier1_code;

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
                        tier1format_gid: tier1format_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier2_code: $scope.cboriskcode,
                        tier2code_changereason: $scope.txtcode_changereason
                    }
                    lockUI();
                    var url = "api/TierMeeting/PostTier2codeChange"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtcode_changereason = '';
                            $scope.codechangereasonshow = false;
                            $scope.tmpcodevisible = false;
                            $scope.tmpdata = true;
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTier2ColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;
                                if (resp.data.tier_code != "") {
                                  
                                    $scope.tmptier_stage = resp.data.tier_stage;
                                    $scope.tmptier_code = resp.data.tier_code;
                                    $scope.tmptiercode_changereason = resp.data.tiercode_changereason;
                                    $scope.tmptier2_codechange = resp.data.tmptier2_codechange
                                }
                                else {
                                    $scope.tmpdata = false;
                                }

                            });
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier2Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier2document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'Y';
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'N';
                    }
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var tier2upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier2UploadCancel';
            SocketService.getparams(url, tier2upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

        $scope.submittier2 = function () {
            lockUI();
            if ($scope.uploadflag != 'Y') {
                Notify.alert('Atleast Upload One Document to Submit..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
            }
            else {
                var vertical_name = $('#cboverticalname :selected').text();
                var headRMD_name = $('#cboemployeename :selected').text();
                var tier2dtl = {
                    zonalmapping_gid: $scope.zonalmapping_gid,
                    zonal_name: $scope.zonal_name,
                    tier2_month: $scope.cbomonth,
                    vertical_gid: $scope.cbovertical_gid,
                    vertical: vertical_name,
                    headRMD_gid: $scope.cboemployeegid,
                    headRMD_name: headRMD_name,
                    tier2_remarks: $scope.txttier2_remarks,
                }

                var url = "api/TierMeeting/PostTier2Preparation";
                SocketService.post(url, tier2dtl).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.txttier2_remarks = "";
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
    }
})();
