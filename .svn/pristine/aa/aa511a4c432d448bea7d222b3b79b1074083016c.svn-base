(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferraledit', manageDeferraledit);

    manageDeferraledit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function manageDeferraledit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferraledit';
        activate();
        var customer_remarks;
        function activate() {
            $('#loandata').multiselect({ includeSelectAllOption: true });


            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats=['dd-MM-yyyy'];
            vm.format=vm.formats[0];

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });

            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
            });

            var param = {
                deferral_gid: localStorage.getItem('deferral_gid')
            };

            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                //console.log(resp.data)
                $scope.Getdeferral = resp.data;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.entityEdit = resp.data.entity_gid;
                $scope.customer = resp.data.customer_gid;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.cluster_manager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.tracking_type = resp.data.tracking_type;
                $scope.loanRefNoedit = resp.data.loanGID;
                $scope.due_dateedit = resp.data.due_date;
                $scope.due_dateedit = Date.parse($scope.due_dateedit);
                if (resp.data.tracking_type == "Covenant") {
                    $scope.showval = true;
                    $scope.hideval = false;
                }
                else {
                    $scope.showval = false;
                    $scope.hideval = true;
                }
                $scope.deferralname = resp.data.deferraltype_gid;
                $scope.covenanttype = resp.data.covenanttype_gid;
                $scope.deferralcategoryedit = resp.data.deferral_category;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.remarks;
                $scope.customerremarks = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                if (resp.data.checker_status == "PushBack") {
                    $scope.showvalchecker_pushback = true;
                }
                else {
                    $scope.showvalchecker_pushback = false;
                }

            });


            $scope.onselectedchangedeferral = function (val) {
                var params = {
                    deferral: val
                };
                var url = 'api/loan/getdeferralcriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }

            $scope.onselectedchangecovenant = function (covenanttype) {
                var params = {
                    covenanttype: covenanttype
                };
                var url = 'api/loan/getcovenanttypecriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }

            $scope.onselectedchangecustomer = function (customer) {
                var params = {
                    customer_gid: customer
                }
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                });
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }
            });
        }
        $scope.btncopy = function () {
            customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
        }
        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
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
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            document.getElementById('load').style.visibility = "visible";
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                    activate();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
                activate();
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
        }
        $scope.deferralback = function () {
            $state.go('app.manageDeferral');
        }
        $scope.deferralUpdateEdit = function () {
            //var date = new Date($scope.due_dateedit);
            //date = date.getFullYear() + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();

            var entity_name_edit;
            var branch_name_edit;
            var deferraltype_name_edit;
            var covenanttype_name_edit;
            var customername_edit;
            var zonalhead_name_edit;
            var businesshead_name_edit;
            var clusterhead_name_edit;
            var relationalmgr_name_edit;
            var creditmanager_name_edit;

            var vertical_code = $('#vertical_code :selected').text();
            var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
            if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
            var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
            if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
            var customer_index = $scope.customer_list.map(function (e) { return e.customer_gid }).indexOf($scope.customer);
            if (customer_index == -1) { customername_edit = ''; } else { customername_edit = $scope.customer_list[customer_index].customername; };
            var zonalhead_index = $scope.zonallist.map(function (e) { return e.employee_gid }).indexOf($scope.zonalHead);
            if (zonalhead_index == -1) { zonalhead_name_edit = ''; } else { zonalhead_name_edit = $scope.zonallist[zonalhead_index].employee_name; }
            var businesshead_index = $scope.businesshead_list.map(function (e) { return e.employee_gid }).indexOf($scope.businessHead);
            if (businesshead_index == -1) { businesshead_name_edit = ''; } else { businesshead_name_edit = $scope.businesshead_list[businesshead_index].employee_name; }
            var clusterhead_index = $scope.clusterlist.map(function (e) { return e.employee_gid }).indexOf($scope.cluster_manager);
            if (clusterhead_index == -1) { clusterhead_name_edit = ''; } else { clusterhead_name_edit = $scope.clusterlist[clusterhead_index].employee_name; }
            var reletinalmgr_index = $scope.relationshiplist.map(function (e) { return e.employee_gid }).indexOf($scope.relationshipMgmt);
            if (reletinalmgr_index == -1) { relationalmgr_name_edit = ''; } else { relationalmgr_name_edit = $scope.relationshiplist[reletinalmgr_index].employee_name; }
            var creditmanager_index = $scope.creditlist.map(function (e) { return e.employee_gid }).indexOf($scope.creditmgmt_name);
            if (creditmanager_index == -1) { creditmanager_name_edit = ''; } else { creditmanager_name_edit = $scope.creditlist[creditmanager_index].employee_name; }
            var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralname);
            if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
            var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttype);
            if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
            if ($scope.tracking_type == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttype = ''; covenanttype_name_edit = ''; };

            var params = {
                deferral_gid: localStorage.getItem('deferral_gid'),
                entity_gid: $scope.entityEdit,
                entity_name: entity_name_edit,
                branch_gid: $scope.branchEdit,
                branch_name: branch_name_edit,
                customer_gid: $scope.customer,
                customer_name: customername_edit,
                loan_gid: $scope.loanRefNoedit,
                zonalhead_gid: $scope.zonalHead,
                zonalhead_name: zonalhead_name_edit,
                businesshead_gid: $scope.businessHead,
                businesshead_name: businesshead_name_edit,
                clustermgr_gid: $scope.cluster_manager,
                clusterhead_name: clusterhead_name_edit,
                relationmgr_gid: $scope.relationshipMgmt,
                relationmgr_name: relationalmgr_name_edit,
                creditmgr_gid: $scope.creditmgmt_name,
                creditmgr_name: creditmanager_name_edit,
                category_gid: $scope.deferralcategoryedit,
                tracking_type: $scope.tracking_type,
                deferraltype_gid: $scope.deferralname,
                deferraltype_name: deferraltype_name_edit,
                covenanttype_gid: $scope.covenanttype,
                covenanttype_name: covenanttype_name_edit,
                duedate: $scope.due_dateedit,
                //duedate: date,
                criticallity: $scope.criticallity,
                vertical_code: vertical_code,
                vertical_gid: $scope.vertical,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                checker_status: $scope.checker_status
            }
            //console.log(params);
            var url = 'api/deferral/update' ;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.manageDeferral');
                    Notify.alert('Deferral Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Deferral !')
                }
            });
        }
    }
})();



