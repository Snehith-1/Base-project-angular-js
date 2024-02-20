(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferraladd', manageDeferraladd);

    manageDeferraladd.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function manageDeferraladd($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferraladd';
        activate();
        var customer_remarks;


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

            vm.formats=['dd-MM-yyyy'];
            vm.format=vm.formats[0];

            //document.getElementById('load').style.visibility = "hidden";
            $('#loandata').multiselect({ includeSelectAllOption: true });
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
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
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


            $scope.onselectedchangecustomer = function (customer) {
                var loandata = '';
                var params = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/deferral/customer2loan';

                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.loan = resp.data.loan;
                });

                $scope.showcustomer = true;
                $scope.showdetails = true;
               
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.vertical = true;
                });

            }




            $scope.onselectedchangedeferral = function (val) {
                if($scope.deferralname==undefined){
                    $scope.MDLcriticallity = '';
                    $scope.criticallity = '';
                    $scope.remarks ='';
                    $scope.customerremarks='';
                    $scope.critical = false;
              
                }
                else{
                    $scope.MDLcriticallity = $scope.deferralname;
                    $scope.criticallity = $scope.deferralname.criticallity;
                    $scope.remarks =$scope.deferralname.comments;
                    $scope.customerremarks=$scope.deferralname.comments;
                    $scope.critical = true
              
                }
              
        }
        $scope.onselectedchangecovenant = function (covenanttype) {
            if($scope.covenanttype==undefined){
                $scope.MDLcriticallity = '';
                $scope.criticallity = '';
                $scope.remarks ='';
                $scope.customerremarks='';
                $scope.critical = false
          
            }
            else{
                $scope.MDLcriticallity = $scope.covenanttype;
                $scope.criticallity = $scope.covenanttype.criticallity;
                $scope.remarks = $scope.covenanttype.comments;
                $scope.customerremarks=$scope.covenanttype.comments;
                $scope.critical = true
          
            }
              
        }


        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }

            var loandata = '';

            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;

            });

            $scope.showcustomer = true;
            $scope.showdetails = true;

            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.vertical = true;
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
                $scope.showdiv = true;
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
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       
        $scope.deferralback = function (val) {
            $state.go('app.manageDeferral');
        }

        $scope.deferralSubmit = function () {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;
            //var customer_name = $('#customer_name :selected').text();
            var deferral_name = $('#deferral_name :selected').text();
            var covenanttype_name = $('#covenanttype_name :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmgmt_name :selected').text();
            var entityname = $('#entity_name :selected').text();
            var branchname = $('#branch_name :selected').text();

            if($scope.trackingtype=='Deferral'){
                if($scope.deferralname==undefined){
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if($scope.trackingtype=='Covenant'){
                if($scope.covenanttype==undefined){
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                deferral_name='';
                deferraltype_gid='';
                 covenanttype_name = $('#covenanttype_name :selected').text();
                 covenanttype_gid=$scope.covenanttype.covenanttype_gid
            }
            var params = {
                entity_gid: $scope.entity.entity_gid,
                branch_gid: $scope.branch.branch_gid,
                entity_name: entityname,
                branch_name: branchname,
                customer_gid: $scope.customer_gid,
                covenanttype_gid: covenanttype_gid,
                criticallity: $scope.criticallity,
                deferraltype_gid: deferraltype_gid,
                loans: $scope.$parent.loan,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                vertical_code: $scope.vertical_code,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferral_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                sanction_refno: $scope.sanctionrefno,
                sanction_date: $scope.sanctiondate,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                customer_name: $scope.customer,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmgmt_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_gid,
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/createDeferral';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    //$("#load").hide();
                  

                    Notify.alert('Deferral Created Successfully..!!', 'success')
                    $state.go('app.manageDeferral');
                }
                else {
                    unlockUI();
                    //$("#load").hide();
                    Notify.alert('Select Atleast One Loan')
                    $state.go('app.manageDeferraladd');
                }

            });
       
          

        }

        $scope.deferralSave = function () {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;

            var loandata = $('#loandata :selected').text();
            //var customer_name = $('#customer_name :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmgmt_name :selected').text();
            var entityname = $('#entity_name :selected').text();
            var branchname = $('#branch_name :selected').text();
            if($scope.trackingtype=='Deferral'){
                if($scope.deferralname==undefined){
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if($scope.trackingtype=='Covenant'){
                if($scope.covenanttype==undefined){
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                deferral_name='';
                deferraltype_gid='';
                 covenanttype_name = $('#covenanttype_name :selected').text();
                 covenanttype_gid=$scope.covenanttype.covenanttype_gid
            }

            var params = {
                entity_gid: $scope.entity.entity_gid,
                branch_gid: $scope.branch.branch_gid,
                entity_name: entityname,
                branch_name: branchname,
                customer_gid: $scope.customer_gid,
                covenanttype_gid:covenanttype_gid,
                criticallity: $scope.criticallity,
                deferraltype_gid: deferraltype_gid,
                loans: $scope.$parent.loan,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                vertical_code: $scope.vertical_code,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferral_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                sanction_refno: $scope.sanctionrefno,
                sanction_date: $scope.sanctiondate,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                customer_name: $scope.customer,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmgmt_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_gid,
                //loandata: loandata
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/createDeferral';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                

                    Notify.alert('Deferral Created Successfully..!!', 'success')
                    $scope.deferralcategory = "";
                    $scope.due_date = "";
                    $scope.deferralname = "";
                    $scope.covenanttype = "";
                    $scope.trackingtype = "";
                    $scope.selectedData = "";
                    $scope.remarks = "";
                    $scope.loan = [];
                    var params = {
                        customer_gid: $scope.customer_gid
                    };
                    var url = 'api/deferral/customer2loan';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loan = resp.data.loan;

                    });
                    $scope.critical = false;
                    $scope.showval = false;
                    $scope.hideval = false;
                    document.getElementById("trackingtype").checked = false;
                    var params = {
                        deferral_gid: ''
                    }
                    var url = 'api/deferral/Getcaddoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.filename_list = resp.data.filename_list;
                        }

                    });
                }
                else {
                    unlockUI();

                    Notify.alert('Select Atleast One Loan')
                    $state.go('app.createDeferral');
                }

            });
        
                   }

    }
})();



