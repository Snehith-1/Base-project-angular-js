(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompanyCrimeCheckRecordAPIController', MstCompanyCrimeCheckRecordAPIController);

        MstCompanyCrimeCheckRecordAPIController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstCompanyCrimeCheckRecordAPIController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompanyCrimeCheckRecordAPIController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {

            var params = {
                institution_gid: $scope.institution_gid
            }

            var url = 'api/CrimeCheckAPI/GetCrimeRecordCompanyDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblcompany_name = resp.data.company_name;               
                $scope.lblcompany_cin = resp.data.company_cin;               
                $scope.companyaddress_list = resp.data.companyaddress_list;         
            }); 

            var url = 'api/CrimeCheckAPI/GetTaggedCaseInstitutionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tagcaseinstitution_list = resp.data.tagcaseinstitution_list;

            });
            

            $scope.rdbSearchmode = "Default";
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting" || lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);

            }

        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/MstDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/MstCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/MstCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/MstCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/MstCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/MstCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/MstCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/MstCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/MstCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/MstCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        //KYC API
        $scope.tan_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }
        $scope.company_addguarantee = function () {
            $location.url('app/MstCreditGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_addcolending = function () {
            $location.url('app/MstCreditColendingDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.CrimeCheck_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/CrimeCheckView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {    
                
                   $scope.lblcinNumber = data.cinNumber;              
                   $scope.lblcaseType = data.caseType;
                   $scope.lblcaseStatus = data.caseStatus;
                   $scope.lblpetitioner = data.petitioner;
                   $scope.lblpetitionerAddress = data.petitionerAddress;
                   $scope.lblrespondent = data.respondent;
                   $scope.lblrespondentAddress = data.respondentAddress;
                   $scope.lblcaseTypeName = data.caseTypeName;
                   $scope.lblcaseName = data.caseName
                   $scope.lblcourtType = data.courtType;
                   $scope.lbldistrict = data.district;
                   $scope.lblstate = data.state;
                   $scope.lblyear = data.year;
                   $scope.lblgfc_updated_at = data.gfc_updated_at;
                   $scope.lblgfc_uniqueid = data.gfc_uniqueid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.CrimeCheck_Tag = function (data) {
            
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to tag this Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Tag it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var params = {
                        institution_gid: $scope.institution_gid,
                        cin_number: data.cinNumber,
                        case_type: data.caseType,
                        case_status: data.caseStatus,
                        petitioner: data.petitioner,
                        petitioner_address: data.petitionerAddress,
                        respondent: data.respondent,
                        respondent_address: data.respondentAddress,
                        casetype_name: data.caseTypeName,
                        case_name: data.caseName,
                        court_type: data.courtType,
                        district: data.district,
                        state: data.state,
                        year: data.year,
                        gfc_updated_at: data.gfc_updated_at,
                        gfc_uniqueid: data.gfc_uniqueid,
                        casedetails_link: data.caseDetailsLink
                    }
                    lockUI();
                    var url = 'api/CrimeCheckAPI/TagCaseInstitution';
                    SocketService.post(url, params).then(function (resp) {
                       unlockUI();
                       if (resp.data.status == true) {
                           SweetAlert.swal('Case Tagged Successfully!');     
                           taggedcaseinstitution_list();        
                           $scope.tagged_cases = true;
                           $scope.search_results = false;           
                       }
                       else {
                           alert(resp.data.message, {
                               status: 'warning',
                               pos: 'top-center',
                               timeout: 3000
                           });   
                       }
                    });
                }
                $scope.search_results = true;
            });
        }

        function taggedcaseinstitution_list() {
            var params = {
                institution_gid: $scope.institution_gid
            }
            var url = 'api/CrimeCheckAPI/GetTaggedCaseInstitutionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tagcaseinstitution_list = resp.data.tagcaseinstitution_list;
            });
        }

        $scope.TaggedCase_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/TaggedCaseView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lblcinNumber = data.cin_number;
                $scope.lblcaseType = data.case_type;
                $scope.lblcaseStatus = data.case_status;
                $scope.lblpetitioner = data.petitioner;
                $scope.lblpetitionerAddress = data.petitioner_address;
                $scope.lblrespondent = data.respondent;
                $scope.lblrespondentAddress = data.respondent_address;
                $scope.lblcaseTypeName = data.casetype_name;
                $scope.lblcaseName = data.case_name
                $scope.lblcourtType = data.court_type;
                $scope.lbldistrict = data.district;
                $scope.lblstate = data.state;
                $scope.lblyear = data.year;
                $scope.lblgfc_updated_at = data.gfc_updated_at;
                $scope.lblgfc_uniqueid = data.gfc_uniqueid;
                $scope.lblcasedetails_link = data.casedetails_link;
                




                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.TaggedCase_Delete = function (crimecasetaggedinstitution_gid) {
            var params =
                {
                    crimecasetaggedinstitution_gid: crimecasetaggedinstitution_gid
                }
            var url = 'api/CrimeCheckAPI/DeleteTaggedCaseInstitution';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    taggedcaseinstitution_list();
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

        $scope.reset_crimecheck = function () {
            $scope.cboaddress = '';
            $scope.rdbSearchmode = '';
            $scope.searchin_petitioner = '';
            $scope.searchin_respondent = '';
        }

        $scope.SearchResults = function () {
            $scope.search_results = true;
            $scope.tagged_cases = false;
        }

        $scope.TaggedCases = function () {
            $scope.tagged_cases = true;
            $scope.search_results = false;
        }

        $scope.search_crimecheck = function () {                    

            var searchIn = '';
            if($scope.searchin_petitioner == true && $scope.searchin_respondent == true) {
                searchIn = 'petitioner,respondent';
            } else if($scope.searchin_petitioner == true && ($scope.searchin_respondent == undefined || $scope.searchin_respondent == false) ) {
                searchIn = 'petitioner';
            } else if($scope.searchin_respondent == true && ($scope.searchin_petitioner == undefined || $scope.searchin_petitioner == false) ) {
                searchIn = 'respondent';
            } else {

            }

            if($scope.cboaddress == undefined || $scope.rdbSearchmode == undefined || searchIn == '') {
                Notify.alert('Kindly enter all the mandatory fields..!', 'warning');
            } 
            else {
                var params = {
                    company_name: $scope.lblcompany_name,
                    company_cin: $scope.lblcompany_cin,
                    company_address: $scope.cboaddress.address,
                    search_mode: $scope.rdbSearchmode,
                    institution_gid: $scope.institution_gid,
                    application_gid: $scope.application_gid,
                    search_in: searchIn                        
                } 
               var url = 'api/CrimeCheckAPI/GetCrimeRecordsCompany';
               lockUI();
               SocketService.post(url, params).then(function (resp) {               
                setTimeout(function() {
                    if (resp.data.requestStatus == true) {
                        $scope.case_list = '';
                        $scope.case_list = resp.data.details;
    
                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });     
                       $scope.search_results = true;
                       $scope.tagged_cases = false;             
                       unlockUI();
                   }
                   else {
                       Notify.alert(resp.data.message, {
                           status: 'warning',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       unlockUI();
                   }
                }, 1000);

                   
               });
            }
   
        } 

        $scope.highlightCompany = function(data, searchTerm) {
            var text = '';
            var text = data.petitioner + ' Vs ' + data.respondent + ' <br /> #' + data.cinNumber + '@' + data.courtType + ',' + data.district;
            searchTerm = searchTerm.trim();
            if (!searchTerm) {
                return $sce.trustAsHtml(text);
            } 
            else {
                 var searchTermArray = searchTerm.split(' ');
                 for (var i = 0; i < searchTermArray.length; i++) { 
                    text = text.replace(new RegExp(searchTermArray[i], 'gi'), '<span class="highlightedText">$&</span>');
                    }
                return $sce.trustAsHtml(text);
            }

            
        };

    }
})();
