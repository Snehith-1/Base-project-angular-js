(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanAddRequestController', MstHRLoanAddRequestController);

    MstHRLoanAddRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstHRLoanAddRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanAddRequestController';

        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid,lshrhead_gid,lsemployee,lsuser_gid,lsentity_gid;
       

        activate();
        function activate() {
             var url = 'api/MstHRLoanRequest/tempdelete';
             SocketService.get(url).then(function (resp) {
             });

            var url = 'api/MstHRLoanRequest/GetFinType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.fintype_list = resp.data.fintype_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_list = resp.data.purpose_list;
                unlockUI();
            });

            var url = 'api/MstHRLoanRequest/GetEmployeeDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.txtemp = resp.data.employee_name;
                $scope.txtrole = resp.data.role;
                $scope.txtdept = resp.data.department;

                $scope.txtofficialmail = resp.data.official_mailid;
                $scope.txtofficialmobile = resp.data.official_mobileno;
                $scope.txtpersonalmail = resp.data.pers_mailid;
                $scope.txtpersonalmobile = resp.data.pers_mobileno;
                $scope.txtentityname = resp.data.entity_name;
                lsentity_gid = resp.data.entity_gid;

                $scope.txtreporting_manager = resp.data.reporting_manager;
                $scope.txtfunctional_head = resp.data.functional_head;                
                lsFunctionalhead_gid = resp.data.functionalhead_gid;                
                lsdepartment_gid = resp.data.department_gid;
                lsreportingmgr_gid = resp.data.reportingmgr_gid;
                lsemployee = resp.data.employee_gid;
                lsuser_gid = resp.data.user_gid; 
                $scope.txtinterest = 0;
                unlockUI();
            });                      
        }



        //Number in words
        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

       
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtamount = "";
            }
            else {
                $scope.txtamount = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }

        // $scope.Gettenure = function (fintype_name) {
        //     lockUI();
        //     var params = {
        //         // fintype_gid:'',
        //         fintype_name: fintype_name
        //     }
        //     var url = 'api/MstHRLoanRequest/GetTenureName';
        //     SocketService.getparams(url, params).then(function (resp) {
        //         if(resp.data.tenure == null || resp.data.tenure == undefined || resp.data.tenure == '')
        //         {                    
        //             Notify.alert('Kindly add tenure for the financial assistance', 'warning');
        //             $scope.txttenure = ''; 
        //         }
        //         else if(resp.data.tenure != null || resp.data.tenure != undefined || resp.data.tenure != '')
        //         {                 
        //             $scope.txttenure = resp.data.tenure;                 
        //         }
                
        //         unlockUI();
        //     });
        // }
        $scope.loanamount = function (fintype_gid) {
            for (var i = 0; i < $scope.fintype_list.length; i++) {
                if (fintype_gid == $scope.fintype_list[i].fintype_gid){
                $scope.selectinterest = $scope.fintype_list[i].fintype_name.replace(" ","").toLowerCase();                
                }
            }
        }
        
        $scope.purposeNote = function(purpose_gid){
            for (var i = 0; i < $scope.purpose_list.length; i++) {
                if (purpose_gid == $scope.purpose_list[i].purpose_gid){
                // $scope.selectNote = $scope.purpose_list[i].purpose_note;
                $scope.selectMandatory = $scope.purpose_list[i].mandatory;
                document.getElementById('selectNote').innerHTML = $scope.purpose_list[i].purpose_note;               
                }            
            }
        }

        $scope.download_allenquiry = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }
     
                // Document Multiple Add
                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
        
            $scope.UploadcompanyDocument = function (val, val1, name) {
                if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                    $("#file").val('');
                    Notify.alert('Kindly Enter the Document Title/ID', 'warning');
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
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                    }
                //     var item = {
                //         name: val[0].name,
                //         file: val[0]
                //     };
                //     var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                //     if (IsValidExtension == false) {
                //         Notify.alert("File format is not supported..!", {
                //             status: 'danger',
                //             pos: 'top-center',
                //             timeout: 3000
                //         });
                //         return false;
                //     }
                
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.txtdocument_title);             
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('request_gid',$scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstHRLoanRequest/RequestDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        unlockUI();
                        $("#file").val('');
                        $scope.txtdocument_title = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;
                         
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                       
                        var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                                SocketService.get(url).then(function (resp) {
                                    $scope.upload_list = resp.data.upload_list;
                                });                           
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
               }
        }

       
        
            $scope.delete_companydocument = function (hrreqdocument_gid) {
                lockUI();
                var params = {
                    hrreqdocument_gid: hrreqdocument_gid
                }
                var url = 'api/MstHRLoanRequest/UploadDocumentsDelete';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                    if (resp.data.status == true) {
        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                                SocketService.get(url).then(function (resp) {
                                    $scope.upload_list = resp.data.upload_list;
                                });                  
                    unlockUI();
                });
            }
        
            
        $scope.submit_request = function () {
           
            var params = {                 
                fintype_gid: $scope.cbofintype.fintype_gid,
                fintype_name: $scope.cbofintype.fintype_name,
                employee_gid: lsemployee,
                employee_name: $scope.txtemp,
                employee_role: $scope.txtrole,
                department_gid: lsdepartment_gid,
                department_name: $scope.txtdept,
                user_gid: lsuser_gid,
                reporting_mgr: $scope.txtreporting_manager,
                reportingmgr_gid: lsreportingmgr_gid,
                functional_head: $scope.txtfunctional_head,
                functionalhead_gid: lsFunctionalhead_gid,
                hr_head: $scope.txthr_head,
                hrhead_gid: lshrhead_gid,
                amount: $scope.txtamount,
                purpose_gid: $scope.cbopurpose.purpose_gid,
                purpose_name: $scope.cbopurpose.purpose_name,
                severity_gid: $scope.cboseverity.severity_gid,
                severity_name: $scope.cboseverity.severity_name,
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason,
                entity_name : $scope.txtentityname,
                entity_gid: lsentity_gid
            }


            var url = "api/MstHRLoanRequest/PostHrloanRequest"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.MstHRLoanRaiseRequest');
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
        
        $scope.saveas_draft = function () {
            var lsfintype_gid = '';
            var lsfintype_name = '';
            if ($scope.cbofintype != undefined || $scope.cbofintype != null) {
                lsfintype_gid = $scope.cbofintype.fintype_gid;
                lsfintype_name = $scope.cbofintype.fintype_name;
            }

            var lspurpose_gid = '';
            var lspurpose_name = '';
            if ($scope.cbopurpose != undefined || $scope.cbopurpose != null) {
                lspurpose_gid = $scope.cbopurpose.purpose_gid;
                lspurpose_name = $scope.cbopurpose.purpose_name;
            }

            var lsseverity_gid = '';
            var lsseverity_name = '';
            if ($scope.cboseverity != undefined || $scope.cboseverity != null) {
                lsseverity_gid = $scope.cboseverity.severity_gid;
                lsseverity_name = $scope.cboseverity.severity_name;
            }            
            
            
            var params = {
                fintype_gid: lsfintype_gid,
                fintype_name: lsfintype_name,
                purpose_gid: lspurpose_gid,
                purpose_name: lspurpose_name,
                severity_gid: lsseverity_gid,
                severity_name: lsseverity_name,
                employee_gid: lsemployee,
                employee_name: $scope.txtemp,
                employee_role: $scope.txtrole,
                department_gid: lsdepartment_gid,
                department_name: $scope.txtdept,
                user_gid: lsuser_gid,
                reporting_mgr: $scope.txtreporting_manager,
                reportingmgr_gid: lsreportingmgr_gid,
                functional_head: $scope.txtfunctional_head,
                functionalhead_gid: lsFunctionalhead_gid,
                hr_head: $scope.txthr_head,
                hrhead_gid: lshrhead_gid,
                amount: $scope.txtamount,                
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason,
                entity_name : $scope.txtentityname,
                entity_gid: lsentity_gid
               
            }


            var url = "api/MstHRLoanRequest/HRLoanRequestSaveasdraft"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });                    
                    $state.go('app.MstHRLoanRaiseRequest');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                }
            });

        }

        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }
    }

})();