(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3Createcontroller', tier3Createcontroller);

    tier3Createcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier3Createcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3Createcontroller';

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
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier3Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
                unlockUI();
            });
        }

        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'N',
                zonalmapping_gid:""
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;
            });
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'N'
            }
            lockUI();
            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
                unlockUI();
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier3Upload';

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
            var tier3upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier3UploadCancel';
            SocketService.getparams(url, tier3upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

        $scope.submittier3 = function () {
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
                var tier3dtl = {
                    MLRC_date: $scope.MLRCdate,
                    tier3_month: $scope.cbomonth,
                    vertical_gid: $scope.cbovertical_gid,
                    vertical: vertical_name,
                    follow_up: $scope.txttier3_followup,
                }
                var url = "api/TierMeeting/PostTier3Preparation";
                SocketService.post(url, tier3dtl).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.tier3Preparation');

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

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, customer_name, customer_urn, tier3_code) {
            
            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtcode_changereason = "";
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
                        $scope.txtcode_changereason = "";
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
                            $scope.txtcode_changereason = "";
                            $modalInstance.close('closed');
                            var params = {
                                vertical_gid: $scope.cbovertical_gid,
                                month: $scope.cbomonth,
                                tier2_flag: 'N'
                            }
                            lockUI();
                            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tierallocationdtl = [];
                                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                                $scope.tier2dtl = true;
                                unlockUI();
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
    }
})();
