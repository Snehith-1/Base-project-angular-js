(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplCreationIndividualGuarantorViewController', MstApplCreationIndividualGuarantorViewController);

        MstApplCreationIndividualGuarantorViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function MstApplCreationIndividualGuarantorViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplCreationIndividualGuarantorViewController';
        var contact_gid = localStorage.getItem('contact_gid');

        lockUI();
        activate();

        function activate() {
             
             var params = {
                contact_gid: contact_gid
            }
           
            var url = 'api/MstApplicationView/GetGurantorIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.individual_name;
                $scope.txtborrower_type = resp.data.borrower_type;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
                var aadhar = $scope.txtaadhar_number;
                var mask = aadhar.slice(-4);
                var maskaadhar = 'XXXX-XXXX-' + mask;
                $scope.guarantoraadhar_number = maskaadhar;
                $scope.txt_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.txtgender = resp.data.gender_name;
                $scope.txtdesignation = resp.data.designation_name;
                $scope.txt_peppoliticallyperson = resp.data.pep_status;
                $scope.txtpep_verifiesdate = resp.data.pepverified_date;
                $scope.txtmarital_status = resp.data.maritalstatus_name;
                $scope.txtfather_name = resp.data.father_name;
                $scope.txtfatherdob_date = resp.data.father_dob;
                $scope.txtfather_age = resp.data.father_age;
                $scope.txtmother_name = resp.data.mother_name;
                $scope.txtmotherdob_date = resp.data.mother_dob;
                $scope.txtmother_age = resp.data.mother_age;
                $scope.txtspouse_name = resp.data.spouse_name;
                $scope.txtspousedob_date = resp.data.spouse_dob;
                $scope.txtspouse_age = resp.data.spouse_age;
                $scope.txtEdu_qualification = resp.data.educationalqualification_name;
                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtannual_income = resp.data.annual_income;
                $scope.lblannual_incomeseperator = (parseInt($scope.txtannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblannual_incomewords = defaultamountwordschange($scope.lblannual_incomeseperator);
                $scope.txtmonthly_income = resp.data.monthly_income;
                $scope.lblmonthly_incomeseperator = (parseInt($scope.txtmonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblmonthly_incomewords = defaultamountwordschange($scope.lblmonthly_incomeseperator);
                $scope.txtincome_type = resp.data.user_type;
                $scope.txtindividualprimary_number = resp.data.primaryindividual_mobileno;
                $scope.individualmobile_list = resp.data.contactmobileno_list;
                $scope.txtindividualprimary_emailaddress = resp.data.primaryindividual_email;
                $scope.individualmailaddress_list = resp.data.contactemail_list;
                $scope.txtownership_type = resp.data.ownershiptype_name;
                $scope.txtproperty_name = resp.data.propertyholder_name;
                $scope.txtresidence_type = resp.data.residencetype_name;
                $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                $scope.txtdistance = resp.data.branch_distance;
                $scope.individualproof_list = resp.data.contactidproof_list;
                $scope.individualdoc_list = resp.data.uploadindividualdoc_list;
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txtindividualbureau_name = resp.data.indbureauname_name;
                $scope.txtindividualbureau_score = resp.data.indbureau_score;
                $scope.txtindividualscore_on = resp.data.indbureauscore_date;
                $scope.txtindividualobservations = resp.data.indobservations;
                $scope.txtindividualbureau_response = resp.data.indbureau_response;
                $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                $scope.individualaddress_list = resp.data.contactaddress_list;
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtprofile = resp.data.profile;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.txtother_nominee = resp.data.othernominee_status;
                $scope.txtrelationship_type = resp.data.relationshiptype;
                $scope.txtnomineedob_date = resp.data.nominee_dob;
                $scope.nomineefirst_name = resp.data.nomineefirst_name;
                $scope.nominee_middlename = resp.data.nominee_middlename;
                $scope.nominee_lastname = resp.data.nominee_lastname;
                $scope.txtnominee_age = resp.data.nominee_age;
                $scope.txtfathernominee_status = resp.data.fathernominee_status;
                $scope.txtmothernominee_status = resp.data.mothernominee_status;
                $scope.txtspousenominee_status = resp.data.spousenominee_status;
                $scope.txttotal_landacres = resp.data.totallandinacres;
                $scope.txtcultivated_land = resp.data.cultivatedland;
                $scope.txtprevious_crop = resp.data.previouscrop;
                $scope.txtproposed_crop = resp.data.prposedcrop;
                $scope.txtinstitution_name = resp.data.institution_name;
                $scope.contactpanabsencereasons_list = resp.data.contactpanabsencereasons_list; 
                $scope.txtpan_status = resp.data.pan_status;
                $scope.txtindnearsamunnati_gid = resp.data.nearsamunnatiabranch_gid;
                $scope.txtindnearsamunnati_name = resp.data.nearsamunnatiabranch_name;
                $scope.txtindphysicalstatus_gid = resp.data.physicalstatus_gid;
                $scope.txtindphysical_status = resp.data.physicalstatus_name;
                $scope.txtindinternalrating_gid = resp.data.internalrating_gid;
                $scope.txtindinternal_rating = resp.data.internalrating_name;
                $scope.mstindlivestockholding_list = resp.data.mstlivestockholding_list;
                $scope.mstindequipmentholding_list = resp.data.mstequipmentholding_list;

                $scope.borrowercontact_gid = resp.data.contact_gid;

                var parambur = {
                    contact_gid: $scope.borrowercontact_gid
                }
                var url = 'api/MstApplicationAdd/GetContactBureauList';
                SocketService.getparams(url, parambur).then(function (resp) {
                    $scope.contactbureau_list = resp.data.contactbureau_list;
                });
            }); 

        }


        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        // function inWords1(num) {
        //     var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        //     var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        //     var s = num.toString();
        //     s = s.replace(/[\, ]/g, '');
        //     if (s != parseFloat(s)) return '';
        //     if ((num = num.toString()).length > 9) return 'Overflow';
        //     var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        //     if (!n) return; var str = '';
        //     str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
        //     str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
        //     str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
        //     str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

        //     str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
        //     return str;
        // }

        $scope.close = function () {
            window.close();
        }

        $scope.individualproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.individualdoc_downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.individualbureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.bureaucontact_view = function (contact2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/IndBureauRespObsDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    contact2bureau_gid: contact2bureau_gid
                };
    
                var url = 'api/MstApplicationEdit/CICIndividualEdit';   
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;
    
                    unlockUI();
                });
                var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });
                $scope.documentviewer = function (val1, val2, val3) {
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

                    if (val3 == 'N') {
                        DownloaddocumentService.DocumentViewer(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
                    }

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            
                $scope.downloadallcicupload = function () {
                    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                        if ($scope.cicuploaddoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name, $scope.cicuploaddoc_list[i].migration_flag);
                        }
                    }
                }
            
                $scope.bureaudoc_downloads = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }


            }

        }

        $scope.equipment_View = function (contact2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2equipment_gid: contact2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetContactEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.livestock_View = function (contact2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2livestock_gid: contact2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetContactLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.downloadallidproof = function () {
            for (var i = 0; i < $scope.individualproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
            }
        }

        $scope.downloadallindividualdoc = function () {
            for (var i = 0; i < $scope.individualdoc_list.length; i++) {
                //DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
                if ($scope.individualdoc_list[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name, $scope.individualdoc_list[i].migration_flag);
                }

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

        $scope.documentviewerinstitution = function (val1, val2, val3) {
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

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
            }

        }


    }
})();
