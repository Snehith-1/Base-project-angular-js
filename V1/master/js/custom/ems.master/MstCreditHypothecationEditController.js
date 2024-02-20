(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditHypothecationEditController', MstCreditHypothecationEditController);

    MstCreditHypothecationEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditHypothecationEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditHypothecationEditController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;


        activate();
        function activate() {

            // Calender Popup... //
            $scope.amount_validation = true;
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MstApplicationAdd/GetproductDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.securitytype_list = resp.data.securitytype_list;
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationEdit/HypothecationDetailsEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cboeditSecurityType = resp.data.securitytype_gid;
                $scope.txteditsecurity_desc = resp.data.security_description;
                $scope.txtsampleeditSecurity_Value = resp.data.security_value;
                $scope.txteditSecurity_Value = (parseInt($scope.txtsampleeditSecurity_Value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txteditSecurity_Value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamountedit6').innerHTML = $scope.lblamountwords;
                $scope.txtSecurityAssessededit_date = resp.data.securityassessed_date;
                $scope.txtassetedit_id = resp.data.asset_id;
                $scope.txtrocedit_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAIedit_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservationedit_summary = resp.data.hypoobservation_summary;
                $scope.txtprimaryedit_security = resp.data.primary_security;
                $scope.DocumentList = resp.data.DocumentList;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
            });
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

       

        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
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

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationAdd/PostHypoDoc';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        hypo_documentList();
                        $scope.cbohypodoc_title = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
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

        function hypo_documentList() {
            var params = { application2hypothecation_gid: $scope.application2hypothecation_gid };
            var url = 'api/MstApplicationEdit/HypothecationDocumentTempList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.DocumentList = resp.data.DocumentList;

            });
        }

        $scope.hypodoccancel = function (val, data) {
            var params = { document_gid: val };
            lockUI();
            var url = 'api/MstApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.hypo_documentList = resp.data.DocumentList;
                    angular.forEach($scope.hypo_documentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.hypo_documentList.splice(key, 1);
                        }
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.update_Hypothecationdtl = function () {

            var securitytype = '';
            var securitytype_gid = '';

            if ($scope.cboeditSecurityType != undefined || $scope.cboeditSecurityType != null) {
                securitytype = $('#security_type :selected').text();

                securitytype_gid = $scope.cboeditSecurityType;
            }

            var lseditSecurity_Value = parseInt($scope.txteditSecurity_Value.replace(/[\s,]+/g, '').trim());

            var params = {
                securitytype_gid: securitytype_gid,
                security_type: securitytype,
                security_description: $scope.txteditsecurity_desc,
                security_value: lseditSecurity_Value,
                securityassessed_date: $scope.txtSecurityAssessededit_date,
                asset_id: $scope.txtassetedit_id,
                roc_fillingid: $scope.txtrocedit_fillingid,
                CERSAI_fillingid: $scope.txtCERSAIedit_fillingid,
                hypoobservation_summary: $scope.txthypoobservationedit_summary,
                primary_security: $scope.txtprimaryedit_security,
                application2hypothecation_gid: $scope.application2hypothecation_gid,
                application_gid: application_gid
            }
            var url = 'api/MstApplicationEdit/HypothecationDetailsUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "myapp") {
                        $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "CreditApproval") {
                        $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "CADApplicationEdit") {
                        $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "CADAcceptanceCustomers") {
                        $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "PendingCADReview") {
                        $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else {

                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }

        $scope.txtSecurityValuechange = function () {
            var input = document.getElementById('SecurityValueedit').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamountedit6 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txteditSecurity_Value = "";
            }
            else {
                // $scope.txtSecurity_Value = output;
                document.getElementById('words_totalamountedit6').innerHTML = lswords_totalamountedit6;
                var txteditSecurity_Value = parseInt($scope.txteditSecurity_Value.replace(/[\s,]+/g, '').trim());
            }
            $scope.txteditSecurity_Value = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadallhyp = function () {
            for (var i = 0; i < $scope.DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DocumentList[i].document_path, $scope.DocumentList[i].document_name);
            }
        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }


    }
})();

