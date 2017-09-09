/**
 * Dialog manager
 */
var dialogManagerJS = new function () {
    var dictDlgs = [];
    var that = this;

    /**
     * Add dialog to storage
     */
    this.addDialog = function (dlgId, $dlg) {
        dictDlgs[dlgId] = $dlg;
    };

    /**
     * Try get dialog 
     */
    this.tryGetDialog = function (dlgId, defaultDlgId) {
        var $dlg = that.getDialog(dlgId);
        if ($dlg == null) {
            $dlg = $("#" + defaultDlgId).clone().prop('id', dlgId);
            that.addDialog(dlgId, $dlg);
        }
        return $dlg;
    };

    /**
     * Get specified dialog
     */
    this.getDialog = function (dlgId) {
        var $dlg = dictDlgs[dlgId];
        return $dlg;
    };

    /**
     * Hide the exist dialog which has been displayed in previous 
     * and display new dialog as model.
     * dlgInstance: the jquery instance of element will be shown (like $('#select-category'))
     */
    this.displayDialog = function (dlgInstance) {
        var $currentModals = $('.modal.in'); // Get current activated model
        if ($currentModals.length > 0) { // If we have active modals
            $currentModals.one('hidden.bs.modal', function () {
                dlgInstance.modal('show'); // Show new model
                dlgInstance.one('hidden.bs.modal', function () { // Triggered when they've finished hiding
                    $currentModals.modal('show'); // When we close the dialog
                });
            }).modal('hide');
        } else { // Otherwise just simply show the modal
            dlgInstance.modal('show');
        }
    };

    /**
     * Close current dialog
     */
    this.closeCurrentDialog = function () {
        // Get current activated model
        var $currentModals = $('.modal.in');
        if ($currentModals.length > 0) { // If we have active modals
            $currentModals.modal('hide');
        }
        return $currentModals;
    };
};