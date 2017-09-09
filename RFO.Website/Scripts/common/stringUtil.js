/**
 * Provides utility APIs
 */
var stringUtilJS = new function () {
    var that = this;

    /**
     * Remove unicode characters
     */
    this.removeUnicodeCharacters = function (s) {
        var arrChar = ['a', 'A', 'd', 'D', 'e', 'E', 'i', 'I', 'o', 'O', 'u', 'U', 'y', 'Y'];
        var uniChar =
        [
            ['á', 'à', 'ả', 'ã', 'ạ', 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ', 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ'],
            ['Á', 'À', 'Ả', 'Ã', 'Ạ', 'Â', 'Ấ', 'Ầ', 'Ẩ', 'Ẫ', 'Ă', 'Ặ', 'Ắ', 'Ằ', 'Ẳ', 'Ẵ', 'Ặ'],
            ['đ'],
            ['Đ'],
            ['é', 'è', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ế', 'ề', 'ể', 'ễ', 'ệ'],
            ['É', 'È', 'Ẻ', 'Ẽ', 'Ẹ', 'Ê', 'Ế', 'Ề', 'Ể', 'Ễ', 'Ệ'],
            ['í', 'ì', 'ỉ', 'ĩ', 'ị'],
            ['Í', 'Ì', 'Ỉ', 'Ĩ', 'Ị'],
            ['ó', 'ò', 'ỏ', 'õ', 'ọ', 'ô', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ', 'ơ', 'ớ', 'ờ', 'ỡ', 'ợ'],
            ['Ó', 'Ò', 'Ỏ', 'Õ', 'Ọ', 'Ô', 'Ố', 'Ồ', 'Ổ', 'Ỗ', 'Ộ', 'Ơ', 'Ớ', 'Ờ', 'Ỡ', 'Ợ'],
            ['ú', 'ù', 'ủ', 'ũ', 'ụ', 'ư', 'ứ', 'ừ', 'ử', 'ữ', 'ự'],
            ['Ú', 'Ù', 'Ủ', 'Ũ', 'Ụ', 'Ư', 'Ứ', 'Ừ', 'Ử', 'Ữ', 'Ự'],
            ['ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ'],
            ['Ý', 'Ỳ', 'Ỷ', 'Ỹ', 'Ỵ']
        ];

        for (var i = 0; i < uniChar.length; i++) {
            for (var j = 0; j < uniChar[i].length; j++) {
                s = s.replace(new RegExp(uniChar[i][j], 'g'), arrChar[i]);
            }
        }

        if (s[s.length - 1] == '_') {
            s = s.substring(0, s.length - 1);
        }

        return s;
    };
    
    /**
     * Truncate a string
     */
    this.truncateContent = function (s, length) {
        var result;
        if (s == null) {
            result = "";
        } else {
            result = s;
            if (s.length > length) {
                result = "...";
                s = s.substring(0, length);
                var lastPos = s.lastIndexOf(' ');
                if (lastPos != -1) {
                    result = s.substring(0, lastPos) + "...";
                }
            }
        }
        return result;
    };
};

