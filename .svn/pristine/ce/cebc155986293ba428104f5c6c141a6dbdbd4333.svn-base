(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionHistoryController', MstSanctionHistoryController);

    MstSanctionHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSanctionHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionHistoryController';

        $scope.application2sanction_gid = $location.search().application2sanction_gid;
        var application2sanction_gid = $scope.application2sanction_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lsresubmit = $location.search().lsresubmit;
        var lsresubmit = $scope.lsresubmit;
        activate();

        function activate() {
            $scope.download_show = false;
            $scope.institutionViewdiv = false;
            var params = {
                application2sanction_gid: application2sanction_gid
            }
            var url = "api/MstCAD/SanctionHistory";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionlisthistory = resp.data.appsanction_list;
            });

           
         
        }

      

        $scope.sanctiondetailsclose = function () {
            $scope.institutionViewdiv = false;
        }
        $scope.sanctiondetails = function (sanctionsubmittoapprovallog_gid, application2sanction_gid) {


            $scope.institutionViewdiv = true;
            var params = {
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstApplicationReport/CADSanctionLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.reportsanctiondetails_list;
            });
            var params = {
                sanctionsubmittoapprovallog_gid: sanctionsubmittoapprovallog_gid
            }
           
            var url = 'api/MstCAD/CADSanctionDtlslog';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanction_refno = resp.data.sanction_refno;
                //$scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.sanctionto_name = resp.data.sanctionto_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.application_no = resp.data.application_no;

                $scope.application_type = resp.data.application_type;
                $scope.contactperson_address = resp.data.contactperson_address;
                $scope.contactperson_name = resp.data.contactperson_name;
                $scope.contactperson_number = resp.data.contactperson_number;
                $scope.contactpersonemail_address = resp.data.contactpersonemail_address;
                $scope.sanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.sanctiontill_date = resp.data.sanctiontill_date;
                $scope.paycard = resp.data.paycard;
                $scope.sanction_type = resp.data.sanction_type;
                $scope.natureof_proposal = resp.data.natureof_proposal;
                $scope.branch_name = resp.data.branch_name;
                $scope.esdeclaration_status = resp.data.esdeclaration_status;

            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var url = 'api/MstCAD/GetTemplateDetailslog';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lspath = resp.data.makerfile_path;
                $scope.lsname = resp.data.makerfile_name;
                $scope.content = resp.data.template_content;
                $scope.checkerletter_flag = resp.data.checkerletter_flag;
                $scope.checkerapproval_flag = resp.data.checkerapproval_flag;
                $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                $scope.sanctionletter_status = resp.data.sanctionletter_status;
                $scope.digitalsignature_flag = resp.data.digitalsignature_flag;
                $scope.checkerupdated_by = resp.data.checkerupdated_by;
                $scope.checkerupdated_on = resp.data.checkerupdated_on;
                $scope.makersubmitted_by = resp.data.makersubmitted_by;
                $scope.makersubmitted_on = resp.data.makersubmitted_on;
                $scope.approved_by = resp.data.approved_by;
                $scope.approved_date = resp.data.approved_date;
                unlockUI();
                console.log('flag', $scope.digitalsignature_flag);
                console.log('lspage', lspage);
                $scope.download_show = true;
            });

          

            var url = 'api/MstCAD/Getesdocumentlog';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadCADES_DocumentList = resp.data.UploadCADES_DocumentList;
            });

            var url = 'api/MstCAD/GetMaildocumentlog';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.DeviationCADMail_DocumentList = resp.data.DeviationCADMail_DocumentList;
            });


          
            var url = 'api/MstCAD/GetApp2SanctionLimitInfoSubmitDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdetails = resp.data.limitandproducts;
            });

            var url = 'api/MstCAD/GetSanctionHistory';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionuploaddocument_list = resp.data.sanctiondocument_list;
            });
           
        
        }

        $scope.addcreditgroup = function () {
            $location.url('app/MstCreditMappingAdd');
        }

        $scope.editcreditgroup = function (creditmapping_gid) {
            $location.url('app/MstCreditMappingEdit?lscreditmapping_gid=' + creditmapping_gid);
        }
     

        $scope.Back = function () {
        
/*            $location.url('app/MstSanctionDtlViewSummary');*/
         /*   $location.url('app/MstSanctionMISReportView?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);*/

            if (lspage == 'SanctionApprovalCompleted') {
                $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=' + lsresubmit);
            }
            else if (lspage == 'SanctionAcceptedCustomer') {
                $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);

            }
            

        }

        $scope.downloadallsanction = function () {
            for (var i = 0; i < $scope.sanctionuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.sanctionuploaddocument_list[i].document_path, $scope.sanctionuploaddocument_list[i].document_name);
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadsCAM = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloaddocument = function (val1, val2) {
            // var phyPath = val1;
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = val2.split(".")
            // link.download = val2;
            // var uri = str;
            // link.href = uri;
            // link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.DeviationCADMail_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DeviationCADMail_DocumentList[i].document_path, $scope.DeviationCADMail_DocumentList[i].document_name);
            }
        }
        $scope.downloadallESdoc = function () {
            for (var i = 0; i < $scope.UploadCADES_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadCADES_DocumentList[i].document_path, $scope.UploadCADES_DocumentList[i].document_name);
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
