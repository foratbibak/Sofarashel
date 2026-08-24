// وقتی یک زیرمجموعه تیک بخوره، والدش هم باید تیک بخوره.
// وقتی تیک یک والد برداشته بشه، همه‌ی زیرمجموعه‌هاش هم باید برداشته بشن.
// وقتی والد تیک بخوره، همه‌ی زیرمجموعه‌هاش هم تیک می‌خورن.
(function () {
    function getCheckbox(id) {
        return document.getElementById(id);
    }

    function checkAllChildren(checkbox, isChecked) {
        var childrenIds = (checkbox.dataset.children || "")
            .split(",")
            .filter(function (id) { return id; });

        childrenIds.forEach(function (childId) {
            var child = getCheckbox(childId);
            if (!child) return;

            child.checked = isChecked;

            if (!isChecked) {
                // اگه والد خاموش شد، کل زیردرخت (فرزندِ فرزند) هم خاموش می‌شه
                checkAllChildren(child, false);
            }
        });
    }

    function checkAllParents(checkbox) {
        var parentId = checkbox.dataset.parent;
        if (!parentId) return;

        var parent = getCheckbox(parentId);
        if (!parent) return;

        parent.checked = true;
        checkAllParents(parent);
    }

    document.addEventListener("change", function (e) {
        var checkbox = e.target;

        if (!checkbox.matches(".permission-checkbox")) {
            return;
        }

        if (checkbox.checked) {
            checkAllChildren(checkbox, true);
            checkAllParents(checkbox);
        } else {
            checkAllChildren(checkbox, false);
        }
    });
})();