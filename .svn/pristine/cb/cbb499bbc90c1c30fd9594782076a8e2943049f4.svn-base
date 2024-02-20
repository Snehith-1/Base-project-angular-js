(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lawyerLegal360controller', lawyerLegal360controller);

    lawyerLegal360controller.$inject = ['$rootScope', '$scope', '$state','$modal', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function lawyerLegal360controller($rootScope, $scope, $state, $modal, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lawyerLegal360controller';

        activate();

        function activate() {
            var params = {
                lawyerlegalSR: localStorage.getItem('raiselegalSR_gid')
            }
            console.log(params);
            var url = "api/lawyerlegalSR/documentlist";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                  
                    $scope.filenamesr_list = resp.data.filenamesr_list;
                }
            });

            //var customer_gid = localStorage.getItem('Lawcustomer_gid');
            $scope.uploaddclickdiv = true;
            localStorage.getItem('LawlegalSR_gid');
            
            var params = {
                customer_gid: localStorage.getItem('Lawcustomer_gid')
            }
            var url = "api/customer/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
            });

            var url = "api/customerManagement/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerPromotorlist = resp.data.customerPromoter;
            });

            var url = "api/customerManagement/getcustomerGuarantors";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerguarantorlist = resp.data.customerGuarantors;
            });

            var url = "api/CustomerDashboard/Getcustomerloandetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loandetails = resp.data.loandtl;
            });

            var url = "api/CustomerDashboard/Getcustomerdocumentdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
            });

            var url = "api/CustomerDashboard/Getcustomermaildetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.composemail_list = resp.data.composemail;
            });

            var url = "api/requestCompliance/lawyerList";
            SocketService.get(url).then(function (resp) {
                $scope.assignlawyerlist = resp.data.assignlawyer;
            });

            var url = "api/CustomerDashboard/GetLawyerPayment";
            SocketService.get(url).then(function (resp) {
                $scope.invoicelist = resp.data.invoicedtl;
            });

            var param = {
                legalSR_gid: localStorage.getItem('LawlegalSR_gid')
            
            }

            var url = "api/CustomerDashboard/GetlawyerSRassign"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assign_lawyername = resp.data.assign_lawyername;
                $scope.assignlawyer = resp.data.assign_lawyergid;
                $scope.assignedlawyer_by = resp.data.assignedlawyer_by;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.SLN_remarks = resp.data.SLN_remarks;
            });

            var url = "api/raiseLegalSR/getSLNdocument";
            SocketService.getparams(url, param).then(function (resp) {
                console.log(resp);
                $scope.seeklawyerdocument = resp.data.uploadseek_list;
            });
        }
 

        $scope.viewinvoice = function (lawyerinvoice_gid) {
            localStorage.setItem('lawyerinvoice_gid', lawyerinvoice_gid)
            $state.go('app.lawyerPaymentView');
        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var params = {
                raiselegalSR_gid: localStorage.getItem('raiselegalSR_gid')
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('raiselegalSR_gid',raiselegalSR_gid);
            $scope.uploadfrm = frm;
            console.log($scope.uploadfrm);
            var url = 'api/lawyerlegalSR/UploadlegalsrDocument';
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                    $scope.filenamesr_list = resp.data.filenamesr_list;
                }
                else {

                    Notify.alert('File Format Not Supported!')
                }

            });
        }
        $scope.document_cancelclick = function (val, data) {
            var params = { tmplegalsr_documentgid: val };
            var url = 'api/lawyerlegalSR/documentdelete'
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {                
                                 Notify.alert('Document Deleted Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.downloads = function (val1, val2) {
            console.log(val1);
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

     

        $scope.viewMailContent = function (composemail_gid) {
            var params = {
                composemail_gid: composemail_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/CustomerViewMailContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                var url = "api/CustomerDashboard/Getcustomermail";
                SocketService.getparams(url, params).then(function (resp) {
                    console.log(resp);
                    $scope.frommail_view = resp.data.from_mail;
                    $scope.tomail_view = resp.data.to_mail;
                    $scope.ccmail_view = resp.data.cc_mail;
                    $scope.bccmail_view = resp.data.bcc_mail;
                    $scope.mailsubject_view = resp.data.subject_mail;
                    $scope.mailcontent_view = resp.data.content_mail;
                    $scope.created_by = resp.data.created_by;
                    $scope.created_date = resp.data.created_date;
                });
            }

        }

    }
})();
