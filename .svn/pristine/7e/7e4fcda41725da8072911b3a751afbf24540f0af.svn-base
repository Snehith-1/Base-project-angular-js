(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loan2deferralcontroller', loan2deferralcontroller);

    loan2deferralcontroller.$inject = ['$rootScope', '$modal', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function loan2deferralcontroller($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loan2deferralcontroller';
        var customer_gid;
        activate();
        var customer_remarks;

        function activate() {
            $scope.activeAdd = true;
            $scope.DivEdittab = false;
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

            $scope.btnupdate = false;
            $scope.btnsubmit = true;
            $scope.loan_gid = localStorage.getItem('loan_gid');
            var url = 'api/loan/getLoandeferraldetails';
            var param = {
                loan_gid: $scope.loan_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.LoanDetails = resp.data;
                $scope.customerName = resp.data.customerName;
                $scope.loanRefNo = resp.data.loanRefNo;
                $scope.loanTitle = resp.data.loanTitle;
                $scope.sanctionRefno = resp.data.sanctionRefno;
                $scope.sanctionDate = resp.data.sanctionDate;
                customer_gid = resp.data.customer_gid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.vertical_gid = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.zonalHead = resp.data.zonal_gid;
                $scope.businessHead = resp.data.businesshead_gid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_gid;
                $scope.creditmanager = resp.data.creditmanager_gid;
                $scope.zonal_name = resp.data.zonal_name,
                $scope.businesshead_name = resp.data.businesshead_name,
                $scope.rm_name = resp.data.rm_name,
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
                $scope.creditmgmt_name = resp.data.creditmgmt_name;
                $scope.entityName = resp.data.entityName;
                $scope.entity_gid = resp.data.entity_gid;
                $scope.branchName = resp.data.branchName;
                $scope.branch_gid = resp.data.branch_gid;
            });



            var url = 'api/deferral/deferralSummary';
            var param = {
                loan_gid: $scope.loan_gid
            };
            SocketService.getparams(url, param).then(function (resp) {
                $scope.deferrals = resp.data.deferralSummaryDtls;
                //console.log(resp.data);
            });
            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });

            var params = {
                deferral_gid: ''
            }
            var url = 'api/deferral/Getcaddoc';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }

            });
            //$scope.showdiv = true;
    }

      

        $scope.onselectedchangedeferral = function () {
            $scope.critical = true;
                $scope.MDLcriticallity = $scope.deferralname;
                $scope.criticallity = $scope.deferralname.criticallity;
                $scope.remarks = $scope.deferralname.comments;
                $scope.customerremarks=$scope.deferralname.comments;
           
            //activate();
        }

        $scope.onselectedchangecovenant = function () {
            $scope.critical = true;
                $scope.MDLcriticallity = $scope.covenanttype;
                $scope.criticallity = $scope.covenanttype.criticallity;
                $scope.remarks = $scope.covenanttype.comments;
                $scope.customerremarks=$scope.covenanttype.comments;
            //activate();
        }
        $scope.btncopy = function () {
            customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
         }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
                $scope.showdiv = true;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
                $scope.showdiv = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
                $scope.showdiv = false;
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
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                    var params = {
                        deferral_gid: $scope.deferral_gid
                    }
                    var url = 'api/deferral/Getcaddoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.filename_list = resp.data.filename_list;
                        }

                    });
                    //$scope.deferral_gid = "";
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                    
                }
                //activate();

            });

        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.viewloan2deferralDetails');

        }
        $scope.deferralback = function (val) {
            $state.go('app.loanManagement');
        }

        $scope.isShowHideAdd=function(val){
            if(val=='Deferral'){
               $scope. showDeferralAdd=true;
               $scope. showCovenantAdd=false;
            }
            else{
               $scope. showDeferralAdd=false;
               $scope. showCovenantAdd=true;
            }
        }
        $scope.isShowHideEdit=function(val){
            if(val=='Deferral'){
               $scope. showDeferralEdit=true;
               $scope. showCovenantEdit=false;
            }
            else{
               $scope. showDeferralEdit=false;
               $scope. showCovenantEdit=true;
            }
        }

        $scope.deferralSubmit = function (val) {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;

            if ($scope.trackingtype == 'Deferral') {
                //console.log($scope.deferralname);
                if ($scope.deferralname == undefined || $scope.deferralname=="") {
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if ($scope.trackingtype == 'Covenant') {
                //console.log($scope.covenanttype);
                if ($scope.covenanttype == undefined || $scope.covenanttype == "") {
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                  covenanttype_name = $('#covenanttype_name :selected').text();
                covenanttype_gid = $scope.covenanttype.covenanttype_gid
                deferral_name='';
                deferraltype_gid='';
                
            }
            
           
            var params = {
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch_gid,
                entity_name: $scope.entityName,
                branch_name: $scope.branchName,
                loan_gid: $scope.loan_gid,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                criticallity: $scope.criticallity,
                vertical_code: $scope.vertical_code,
                customer_gid: $scope.customer_gid,
                covenanttype_gid: covenanttype_gid,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferraltype_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                zonal_name: $scope.zonal_name,
                businesshead_name: $scope.businesshead_name,
                relationshipmgmt_name: $scope.rm_name,
                cluster_manager_name: $scope.cluster_manager_name,
                creditmgmt_name: $scope.creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmanager,
                customer_name: $scope.customerName
               
            }
            //console.log(params);
            if ($scope.trackingtype == "Deferral") {
                    lockUI();
                    //$("#load").show();
                    var url = 'api/deferral/loan2Deferral';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            //$("#load").hide();
                            unlockUI();
                            Notify.alert('Deferral Created Successfully..!!', 'success')
                            $scope.entity = "";
                            $scope.branch = "";
                            $scope.trackingtype = "";
                            $scope.deferralname.deferral_gid = "";
                            $scope.deferralname = "";
                            //$scope.covenanttype.covenanttype_gid = "";
                            $scope.deferralcategory = "";
                            $scope.due_date = "";
                            $scope.critical = false;
                            $scope.showval = false;
                            $scope.hideval = false;
                            document.getElementById("deferralforms").reset();
                            //activate();
                        }
                        else {
                            //$("#load").hide();
                            unlockUI();
                            Notify.alert('Error Occurred While Creating Deferral!')
                            document.getElementById("deferralforms").reset();
                        }
                        activate();
                    });
                
            }
            else if ($scope.trackingtype == "Covenant") {
                    lockUI();
                    //$("#load").show();
                    var url = 'api/deferral/loan2Deferral';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            //$("#load").hide();
                            Notify.alert('Deferral Created Successfully..!!', 'success')
                            $scope.entity = "";
                            $scope.branch = "";
                            $scope.trackingtype = "";
                            //$scope.deferralname.deferral_gid = "";
                            $scope.covenanttype.covenanttype_gid = "";
                            $scope.covenanttype = "";
                            $scope.deferralcategory = "";
                            $scope.due_date = "";
                            $scope.critical = false;
                            $scope.showval = false;
                            $scope.hideval = false;
                            document.getElementById("deferralforms").reset();
                            //activate();
                        }
                        else {
                            unlockUI();
                            //$("#load").hide();
                            Notify.alert('Error Occurred While Creating Deferral!', 'warning')
                            document.getElementById("deferralforms").reset();
                        }
                        activate();

                    });
                
            }

        }

        $scope.popupEdit = function (val) {
            $scope.deferral_gid = val;
            $scope.activeAdd = false;
            $scope.DivEdittab = true;
            
            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
            });

            $scope.entity_list_edit= $scope.entity_list;
            $scope.branch_list_edit = $scope.branch_list;
            $scope.deferral_list_edit = $scope.deferral_list;
            $scope.covenanttype_list_edit = $scope.covenanttype_list;
            var param = {
                deferral_gid: val
            };
            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.entityEdit = resp.data.entity_gid;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.deferralcategoryEdit = resp.data.deferral_category;
                $scope.trackingtypeEdit = resp.data.tracking_type;
                $scope.deferralnameEdit = resp.data.deferraltype_gid;
                $scope.covenanttypeEdit = resp.data.covenanttype_gid;
                $scope.due_dateEdit = resp.data.due_date;
                $scope.due_dateEdit = Date.parse($scope.due_dateEdit);
                $scope.criticallity = resp.data.criticallity;
                $scope.remarksEdit = resp.data.remarks;
                $scope.customerremarksEdit = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                if (resp.data.checker_status == "PushBack") {
                    $scope.showvalchecker_pushback = true;
                }
                else {
                    $scope.showvalchecker_pushback = false;
                }
                if($scope.trackingtypeEdit=='Deferral'){
                    $scope. showDeferralEdit=true;
                    $scope. showCovenantEdit=false;
                 }
                 else{
                    $scope. showDeferralEdit=false;
                    $scope. showCovenantEdit=true;
                 }
                
                 var url = 'api/deferral/Getcaddoc';
                SocketService.getparams(url, param).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.filename_list = resp.data.filename_list;
                    }
                });
            });
        };
        $scope.loan2deferralUpdateEdit = function () {
            //var date = new Date($scope.due_dateEdit);
            //date = date.getFullYear() + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
           
            var deferraltype_name_edit;
            var covenanttype_name_edit;

            //var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
            //if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
            //var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
            //if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
            var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralnameEdit);
            if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
            var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttypeEdit);
            if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
            if ($scope.trackingtypeEdit == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttypeEdit = ''; covenanttype_name_edit = ''; };

            var params = {
                deferral_gid: $scope.deferral_gid,
                entity_gid: $scope.entity_gid,
                entity_name: $scope.entityName,
                branch_gid: $scope.branch_gid,
                branch_name: $scope.branchName,
                customer_gid: $scope.customer_gid,
                customer_name: $scope.customerName,
                loan_gid: $scope.loan_gid,
                zonalhead_gid: $scope.zonalHead,
                zonalhead_name: $scope.zonal_name,
                businesshead_gid: $scope.businessHead,
                businesshead_name: $scope.businesshead_name,
                clustermgr_gid: $scope.clustermanager,
                clusterhead_name: $scope.cluster_manager_name,
                relationmgr_gid: $scope.relationshipMgmt,
                relationmgr_name: $scope.rm_name,

                creditmgr_gid: $scope.creditmanager,
                creditmgr_name: $scope.creditmgmt_name,
                category_gid: $scope.deferralcategoryEdit,
                tracking_type: $scope.trackingtypeEdit,
                deferraltype_gid: $scope.deferralnameEdit,
                deferraltype_name: deferraltype_name_edit,
                covenanttype_gid: $scope.covenanttypeEdit,
                covenanttype_name: covenanttype_name_edit,
                duedate: $scope.due_dateEdit,
                criticallity: $scope.criticallity,
                vertical_gid: $scope.vertical_gid,
                vertical_code: $scope.vertical_code,
                remarks: $scope.remarksEdit,
                customerremarks: $scope.customerremarksEdit,
                checker_status: $scope.checker_status
            }
            //console.log(params);
            var url = 'api/deferral/update' ;
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    document.getElementById("critical").style.visibility = "hidden";
                    $scope.activeAdd = true;
                    $scope.DivEdittab = false;
                    $scope.deferral_gid = "";
                    Notify.alert('Deferral Updated Successfully..!!', 'success')
                }

                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Updating Deferral !')
                }
            });
        }
    }
})();