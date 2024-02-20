(function () {
    'use strict';

    angular
        .module('angle')
        .controller('defapp', defapp);

    defapp.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function defapp($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'defapp';

        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            $scope.tracking_type = localStorage.getItem('tracking_type');

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getDeferraldetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.customerremarks = resp.data.customer_remarks;
                $scope.approval_remarks = resp.data.remarks;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_status = resp.data.approval_status;
                $scope.def_status = resp.data.def_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.deferral_approver_name = resp.data.cluster_manager_name;
                $scope.tracking_type = resp.data.tracking_type;

                if (resp.data.tracking_type == "Covenant") {
                    $scope.showrdb = true;
                }
                else {
                    $scope.showrdb = false;
                }

                if (resp.data.approval_status == "Approval Pending" || resp.data.approval_status == "Extension Approval Pending") {
                    $scope.uploaddoc = true;
                }
                else {
                    $scope.uploaddoc = false;
                }
                if (resp.data.approval_status == "Closed") {
                    $scope.uploaddocument = false;
                }
                else {
                    $scope.uploaddocument = true;
                }

            });

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
                if ($scope.checker_list == null) {
                    $scope.approval_history = true;
                }
                else {
                    $scope.approval_history = false;
                }
            });


            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.stage_list = resp.data.stage_list;
                }
                else {
                    document.getElementById("stages").style.display = "none";
                }
            });

        }

        $scope.upload = function (val, val1, name) {
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
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('by', "cad");
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

            var url = 'api/deferral/uploaddeferraldocumentbycad';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
            });
        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
            // Close Modals
        }

        $scope.close = function (val) {
            document.getElementById("userform").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }
        $scope.Approve = function () {

            if ($scope.tracking_type == "Deferral" && $scope.deferral_status == "Extend") {

                if ($scope.extened_date == undefined) {
                    Notify.alert('Enter Extended Date')
                }
                else {

                    var params = {
                        def_gid: $scope.deferral_gid,
                        deferral_status: $scope.deferral_status,
                        due_date: $scope.extened_date,
                        approval_remarks: $scope.approval_remarks,
                        customer_remarks: $scope.customerremarks
                    }
                    var url = 'api/deferral/deferralApprove';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $state.go('app.cadApproval');
                            Notify.alert('Submitted Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Extended Date must be greater than Due Date!')
                        }
                    });
                }
            }
            else if ($scope.tracking_type == "Covenant" && $scope.deferral_status == "Extend") {
                if ($scope.extened_date == undefined) {
                    Notify.alert('Enter Extended Date')
                }
                else if ($scope.rdb_extendtype == undefined) {
                    Notify.alert('Select Extend Type')
                }
                else {

                    var params = {
                        def_gid: $scope.deferral_gid,
                        deferral_status: $scope.deferral_status,
                        due_date: $scope.extened_date,
                        approval_remarks: $scope.approval_remarks,
                        customer_remarks: $scope.customerremarks,
                        extend_type: $scope.rdb_extendtype,
                    }
                    var url = 'api/deferral/deferralApprove';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $state.go('app.cadApproval');
                            Notify.alert('Submitted Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Extended Date must be greater than Due Date!')
                        }
                    });
                }
            }
            else {

                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    due_date: $scope.extened_date,
                    approval_remarks: $scope.approval_remarks,
                    customer_remarks: $scope.customerremarks
                }

                var url = 'api/deferral/deferralApprove';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $state.go('app.cadApproval');
                        Notify.alert('Submitted Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error Occurred While Submitting!')
                    }
                });
            }


        }




        $scope.deferralback = function (val) {
            $state.go('app.cadApproval');
        }


        $scope.onselectedchange = function (deferral_status) {
            if (deferral_status == "Extend") {
                $scope.showval = true;
            }
            else {
                $scope.showval = false;
            }
        }


    }
})();
