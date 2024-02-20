(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamAllocatedTotransferController', osdBamAllocatedTotransferController);
    osdBamAllocatedTotransferController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function osdBamAllocatedTotransferController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'osdBamAllocatedTotransferController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var bankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
        var customer_gid = searchObject.lscustomer_gid;
        var customer_urn = searchObject.lscustomer_urn;
        var lstab = searchObject.lstab;
        activate();
        lockUI();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
           
            var url = 'api/OsdTrnBankAlert/GetAllocatedDetail';
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid,
                customer_urn: customer_urn,
            }

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.lblticketref_no = resp.data.ticketref_no;
                $scope.lblemail_from = resp.data.email_from;
                $scope.lblemail_date = resp.data.email_date;
                $scope.lblemail_subject = resp.data.email_subject;
                $scope.lblemail_content = resp.data.email_content;
                $scope.lblaging = resp.data.aging;
                $scope.lblrelationshipmanager_name = resp.data.relationshipmanager_name;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lblcustomer_urn = resp.data.customer_urnname;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblvertical = resp.data.vertical;
                $scope.lblconstitution = resp.data.constitution;
                $scope.lblcontact_person = resp.data.contact_person;
                $scope.lblzonal_head = resp.data.zonal_head;
                $scope.lblbusiness_head = resp.data.business_head;
                $scope.lblrm_name = resp.data.rm_name;
                $scope.lblcluster_manager = resp.data.cluster_manager;
                $scope.lblcredit_manager = resp.data.credit_manager;
                $scope.lblzonal_riskmanagerName = resp.data.zonal_riskmanagerName;
                $scope.lblriskmanager_name = resp.data.riskmanager_name;
                $scope.lblriskMonitoring_Name = resp.data.riskMonitoring_Name;
                $scope.lblmobile_no = resp.data.mobile_no;
                $scope.lbladdress_type = resp.data.address_type;
                $scope.lbladdressline1 = resp.data.addressline1;
                $scope.lbladdressline2 = resp.data.addressline2;
                $scope.lblcity = resp.data.city;
                $scope.lblstate = resp.data.state;
                $scope.lbltaluka = resp.data.taluka;
                $scope.lbldistrict = resp.data.district;
                $scope.lblpostal_code = resp.data.postal_code;
                $scope.lblcountry = resp.data.country;
                $scope.lblemail_cc = resp.data.email_cc;
                $scope.lblemail_bcc = resp.data.email_bcc;
                $scope.lbldocument_path = resp.data.document_path;
                $scope.lbldocument_name = resp.data.document_name;
                $scope.lblemail_to = resp.data.email_to;
                $scope.lblfrom_mailaddress = resp.data.from_mailaddress;
                $scope.lbloperation_status = resp.data.operation_status;
                $scope.servicerequest_gid = resp.data.servicerequest_gid;
                $scope.transferto_name = resp.data.transferto_name;
                $scope.employee_gid = resp.data.employee_gid;
                $scope.relationshipmanager_gid = resp.data.relationshipmanager_gid;
                $scope.lblcustomer_name = resp.data.customername_application;
                $scope.lblvertical = resp.data.vertical_name;
                $scope.lblconstitution = resp.data.constitution_name;
                $scope.lblcontact_person = resp.data.contactpersonfirst_name;
                $scope.lblrm_name = resp.data.drm_name;
                $scope.lblrelationshipmanager_name = resp.data.relationship_managername;
                $scope.lblcluster_manager = resp.data.clustermanager_name;
                $scope.lblcredit_manager = resp.data.creditmanager_name;
                $scope.lblzonal_riskmanagerName = resp.data.zonalriskmanager_name;
                $scope.lblriskmanager_name = resp.data.risk_managername;
                $scope.lblriskMonitoring_Name = resp.data.headriskmonitoring_name;
                $scope.lblregional_head = resp.data.regionalhead_name;
                $scope.lblzonal_head = resp.data.zonalhead_name;
                $scope.lblbusiness_head = resp.data.businesshead_name;
                $scope.lblcredithead_name = resp.data.credithead_name;
                $scope.lblcreditnationalmanager_name = resp.data.creditnationalmanager_name;
                $scope.lblcreditregionalmanager_name = resp.data.creditregionalmanager_name;
                unlockUI();
                if ($scope.lblallocated_status == 'Completed') {
                    var url = 'api/OsdTrnBankAlert/GetRMStatusDtl';
                    var param = {
                        bankalert2allocated_gid: bankalert2allocated_gid,
                    }
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lblrm_remarks = resp.data.rm_remarks;
                        // $('#lblrm_remarks').html(resp.data.rm_remarks);
                        $scope.lblrm_status = resp.data.rm_status;
                        $scope.lblupdated_date = resp.data.updated_date;
                        $scope.lblupdated_by = resp.data.updated_by;
                        $scope.rmdocument_list = resp.data.rmdocument_list;
                        $scope.lblrmhfilename = resp.data.rmhfilename;
                        $scope.lblrmhfilepath = resp.data.rmhfilepath;
                    });

                    var url = "api/OsdTrnMyTicket/GetRequestorlist"
                    var param = {
                        servicerequest_gid: $scope.servicerequest_gid,
                    }
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.requestorlist = resp.data.requestordtl;
                        unlockUI();
                    });
                    var param = {
                        servicerequest_gid: $scope.servicerequest_gid,
                    }
                    var url = 'api/OsdTrnMyTicket/GetCompletedDetails';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
                        $scope.completed_remarks = resp.data.completed_remarks;
                        $scope.completed_by = resp.data.completed_by;
                        $scope.completed_date = resp.data.completed_date;
                        $scope.lblfilename = resp.data.compfilename;
                        $scope.lblfilepath = resp.data.compfilepath;
                        unlockUI();
                    });
                }
                var url = 'api/OsdTrnBankAlert/GetOperationStatusDtl';
                var param = {
                    bankalert2allocated_gid: bankalert2allocated_gid,
                }
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lblassigned_remarks = resp.data.assigned_remarks;
                    $scope.lblassigned_date = resp.data.assigned_date;
                    $scope.lblassigned_toname = resp.data.assigned_toname;
                    $scope.lblassigned_by = resp.data.assigned_by;
                    $scope.document_list = resp.data.rmdocument_list;
                });
                var param = {
                    servicerequest_gid: $scope.servicerequest_gid,
                }
                var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

                SocketService.getparams(url, param).then(function (resp) {
                    $scope.transferlistdtl = resp.data.transferlistdtl;
                    $scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
                    unlockUI();
                });
            });
            var url = 'api/OsdTrnBankAlert/GetticketTransferLog'
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.relationshipmanager_name = resp.data.relationshipmanager_name;
                $scope.transferedinitiated_by = resp.data.transferedinitiated_by;
                $scope.transferinitiated_date = resp.data.transferinitiated_date;
                $scope.transfer_type = resp.data.transfer_type;
                $scope.transfer_remarks = resp.data.transfer_remarks;
              });
        }
        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;
            }

        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.Back = function () {
            $location.url("app/osdBamAllocatedToRM?lstab=" + lstab);
        }
        //$scope.download = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("EMS");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        //$scope.download = function (val1, val2) {
        //    var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
        //    var link = document.createElement("a");
        //    link.href = relpath2;
        //    link.setAttribute('download', val2);
        //    link.click();
        //}
        $scope.download = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
         //$scope.downloadsdocument = function (val1, val2) {
         //   var phyPath = val1;

         //   var relPath = phyPath.split("EMS");
         //   var relpath1 = relPath[1].replace("\\", "/");
         //   var hosts = window.location.host;
         //   var prefix = location.protocol + "//";
         //   var str = prefix.concat(hosts, relpath1);
         //   var link = document.createElement("a");
         //   var name = val2.split(".")
         //   link.download = val2;
         //   var uri = str;
         //   link.href = uri;
         //   link.click();
         //}
         //$scope.downloadsdocument = function (val1, val2) {
         //    var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
         //    var link = document.createElement("a");
         //    link.href = relpath2;
         //    link.setAttribute('download', val2);
         //    link.click();
         //}
         $scope.downloadsdocument = function (val1, val2) {
             DownloaddocumentService.Downloaddocument(val1, val2);
         }
       }
})();
