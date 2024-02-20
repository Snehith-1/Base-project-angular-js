(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdComposeMailController', osdComposeMailController);

    osdComposeMailController.$inject = ['DownloaddocumentService','$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout', '$window', 'cmnfunctionService'];

    function osdComposeMailController(DownloaddocumentService,$rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, $window, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'osdComposeMailController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var email_gid = searchObject.email_gid;
        var toMail = searchObject.toMail;
        var ccMail =searchObject.ccMail;
        var bccMail = searchObject.bccMail;
        var email_subject = searchObject.email_subject;
        var message_id = searchObject.message_id;
        var reference_id = searchObject.reference_id;
        var rmemployee_gid = searchObject.rmemployee_gid;
        var rmemployee_name = searchObject.rmemployee_name;
        var decision = searchObject.decision;
        var lspage = searchObject.lspage;
        var llspage = searchObject.llspage;

        $scope.lsShowBCC = true;
        $scope.lsShowCC = true;
    
        activate();

        function activate() {
            $scope.email_gid = email_gid;
            $scope.toMail = toMail;
            $scope.ccMail = ccMail;
            $scope.bccMail = bccMail;
            $scope.email_subject = email_subject;
            $scope.message_id = message_id;
            $scope.reference_id = reference_id;
            $scope.rmemployee_gid = rmemployee_gid;
            $scope.rmemployee_name = rmemployee_name;
            $scope.decision = decision;
            $scope.lspage = lspage;
            $scope.llspage = llspage;

         var url = 'api/OsdTrnCustomerQueryMgmt/GetEmailSignature';
                SocketService.get(url).then(function (resp) {
                    
                    $scope.pushbackcontent = resp.data.emailsignature;
                   
                });
                var url = 'api/OsdTrnCustomerQueryMgmt/Mailtempdelete';
                SocketService.get(url).then(function (resp) {
                });
        }

        $scope.uploadattachment = function (val,val1,name) {
            
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {

                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                   
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "Default");

                        if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                    
                }
                frm.append('project_flag', "Default");
                lockUI();
                var url = 'api/OsdTrnCustomerQueryMgmt/MailAttachment';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                       
                        var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';
            
                        SocketService.get(url).then(function (resp) {
                           
                            $scope.uploaddocument = resp.data.MdlDocDetails;
                          
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')
            
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }        
        }

        $scope.updateDesicion=function(){
            
            if($scope.pushbackcontent==undefined){
                Notify.alert('Write the body of the content','success');
                return;
            }
          
            var params={
                email_gid:$scope.email_gid,
                decision:$scope.decision,
                employee_gid:$scope.rmemployee_gid,
                employee_name:$scope.rmemployee_name,
                remarks:'',
                mailcontent:$scope.pushbackcontent,
                subject:$scope.email_subject,
                tomail_id:$scope.toMail,
                ccmail_id:$scope.ccMail,
                bccmail_id:$scope.bcc_mail,
                message_id:$scope.message_id,
                reference_id:$scope.reference_id
            }
            var url='api/OsdTrnCustomerQueryMgmt/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                   
                    Notify.alert(resp.data.message,'success')

                    
                                
                    if($scope.llspage == "forward")
                    {
                       
                        $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid ));
                    }
                    else if($scope.llspage == "reply")
                    {
                        $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                     
                    }
                    else if($scope.llspage == "transfer")
                    {
                        $location.url('app/osdCqmTransferSummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                 
                    }
                   
                }
                else{
                                 
                    Notify.alert(resp.data.message,'warning')    

                    if($scope.llspage == "forward")
                    {
                        $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                        
                    }
                    else if($scope.llspage == "reply")
                    {
                        $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                       
                    }
                    else if($scope.llspage == "transfer")
                    {
                        $location.url('app/osdCqmTransferSummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                       
                    }
                }
            });


        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/DeleteAttachment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')

                }

            });
        }
        
        $scope.closeDesicion = function () {
            $window.close();
        }

        $scope.downloads = function (path, attchment_name) {
            DownloaddocumentService.Downloaddocument(path, attchment_name);
            // var phyPath = path;
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = attchment_name.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click();
        }

        $scope.back = function() {
               
            if($scope.lspage =="forward")
            {
                $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                
            }
            else if($scope.lspage =="reply")
            {
                $location.url('app/osdCqmReplaySummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                
            }
            else if($scope.lspage == "transfer")
            {
                $location.url('app/osdCqmTransferSummary?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
               
            }
            else if($scope.lspage=="assignedquery360")
            {
                $location.url('app/osdCqmAssignedQuery360?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
                
            }
            else if($scope.lspage=="replyview")
            {
                $location.url('app/osdCqmReplayView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
               
            }
            else if($scope.lspage=="forwardview")
            {
                $location.url('app/osdCqmForwardView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
               
            }
            else if($scope.lspage=="transferview")
            {
                $location.url('app/osdCqmTransferView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
               
            }
        } 

    }
})();
