'use strict'
angular
    .module('angle')
.factory('cmnfunctionService', ['SocketService', '$rootScope', 'apiManage', '$state', '$cookieStore', 'ngDialog',
    function (SocketService, $rootScope, apiManage, $state, $cookieStore, ngDialog) {
        var objservice = {};

        //Pattern Validation for email address
        $rootScope.appemail_pattern = '[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}';
        
        // To Get the difference Between two Dates

        objservice.fnDatediff = function (startingDate, endingDate) {
            if (!endingDate) {
                return '1'; // Empty End Date
            }
            else if (!startingDate) {
                return '1'; // Empty Start Date
            }
            var startDate = new Date(new Date(startingDate).toISOString().substr(0, 10));
            var endDate = new Date(endingDate);
            if (startDate > endDate) {
                return '0' // Start date greater than End Date
            }
            var startYear = startDate.getFullYear();
            var february = (startYear % 4 === 0 && startYear % 100 !== 0) || startYear % 400 === 0 ? 29 : 28;
            var daysInMonth = [31, february, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            var yearDiff = endDate.getFullYear() - startYear;
            var monthDiff = endDate.getMonth() - startDate.getMonth();
            if (monthDiff < 0) {
                yearDiff--;
                monthDiff += 12;
            }
            var dayDiff = endDate.getDate() - startDate.getDate();
            if (dayDiff < 0) {
                if (monthDiff > 0) {
                    monthDiff--;
                } else {
                    yearDiff--;
                    monthDiff = 11;
                }
                dayDiff += daysInMonth[startDate.getMonth()];
            }

            return yearDiff + ' Years ' + monthDiff + ' Months ' + dayDiff + ' Days';
        }


        // To Check the Document Validation

        objservice.fnCheckValidDocType = function (fileName, projectFlag) {
            var str = '';
            if (projectFlag == "")
                projectFlag = "Default";
            var allowed_extensions = [ 
             { project: "Default", extension: "pdf,jpg,png,jpeg,odt,csv,msg,xls,xlsx,txt,ppt,pptx,doc,docx,oft,html" },
             { project: "RSK", extension: "xlsx" },
			 { project: "photoformatonly", extension: "pdf,jpg,png,jpeg" },
             { project: "photo", extension: "jpg,png,jpeg" },
             { project: "documentformatonly", extension: "pdf,jpg,png,jpeg,odt,csv,msg,xls,xlsx,txt,ppt,pptx,doc,docx,oft,html"},
             { project: "BD", extension: "pdf,jpg,png,jpeg,odt,csv,msg,xls,xlsx,txt,ppt,pptx,doc,docx,oft,html,mp3" },
             { project: "DocumentViewerFormat", extension: "pdf,jpg,png,jpeg,txt,html" }];
            var file_extension = fileName.split('.').pop();
            var checkFileExtension = allowed_extensions.filter(function (c) { return c.project === projectFlag });
            if (checkFileExtension && checkFileExtension.length != 0) {
                str = checkFileExtension[0].extension;
                var hasvalid = str.indexOf(file_extension.toLowerCase()) != -1; // true
               if (hasvalid)
                    return true; // valid file extension
                else
                    return false;
            }
        }

        // To Convert Number into Word [Indian Standard]

        objservice.fnConvertNumbertoWord = function (price) {
            var sglDigit = ["Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"],
        dblDigit = ["Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"],
        tensPlace = ["", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"],
        handle_tens = function (dgt, prevDgt) {
            return 0 == dgt ? "" : " " + (1 == dgt ? dblDigit[prevDgt] : tensPlace[dgt])
        },
        handle_utlc = function (dgt, nxtDgt, denom) {
            return (0 != dgt && 1 != nxtDgt ? " " + sglDigit[dgt] : "") + (0 != nxtDgt || dgt > 0 ? " " + denom : "")
        }; 
            var str = "",
              digitIdx = 0,
              digit = 0,
              nxtDigit = 0,
              words = [];
            if (price += "", isNaN(parseInt(price))) str = "";
            else if (parseInt(price) > 0 && price.length <= 10) {
                for (digitIdx = price.length - 1; digitIdx >= 0; digitIdx--) switch (digit = price[digitIdx] - 0, nxtDigit = digitIdx > 0 ? price[digitIdx - 1] - 0 : 0, price.length - digitIdx - 1) {
                    case 0:
                        words.push(handle_utlc(digit, nxtDigit, ""));
                        break;
                    case 1:
                        words.push(handle_tens(digit, price[digitIdx + 1]));
                        break;
                    case 2:
                        words.push(0 != digit ? " " + sglDigit[digit] + " Hundred" + (0 != price[digitIdx + 1] && 0 != price[digitIdx + 2] ? " and" : "") : "");
                        break;
                    case 3:
                        words.push(handle_utlc(digit, nxtDigit, "Thousand"));
                        break;
                    case 4:
                        words.push(handle_tens(digit, price[digitIdx + 1]));
                        break;
                    case 5:
                        words.push(handle_utlc(digit, nxtDigit, "Lakh"));
                        break;
                    case 6:
                        words.push(handle_tens(digit, price[digitIdx + 1]));
                        break;
                    case 7:
                        words.push(handle_utlc(digit, nxtDigit, "Crore"));
                        break;
                    case 8:
                        words.push(handle_tens(digit, price[digitIdx + 1]));
                        break;
                    case 9:
                        words.push(0 != digit ? " " + sglDigit[digit] + " Hundred" + (0 != price[digitIdx + 1] || 0 != price[digitIdx + 2] ? " and" : " Crore") : "")
                }
                str = words.reverse().join("")
            }
            else str = ""; 
            return str;
        }

        objservice.encryptURL = function (data) {
            var encryptedString = window.btoa(data);
            return encryptedString;
        }

        objservice.decryptURL = function (data) {
            var decryptedString = window.atob(data);
            var decryptedObject = JSON.parse('{"' + decodeURI(decryptedString.replace(/&/g, "\",\"").replace(/=/g,"\":\"")) + '"}');
            return decryptedObject;
        }

        return objservice;

    }]);
