class Table {
    constructor(options) {
        options = options || {};
        this.columns = options.columns;
        this.data = options.data;
        this.sort = null;
        let table = document.createElement("table");

        if (options.caption && options.caption.name) {

            this.initCaption(table, options.caption, true);
        }
        this.initThead(this, table, options.columns);
        this.initTbody(this, table, options.data);
        this.delteCaption = function () {
            table && table.deleteCaption && table.deleteCaption();
        };
        this.deleteThead = function () {
            table && table.deleteTHead && table.deleteTHead();
        }

        this.caption = function (nameOrOptions) {
            if (!table) {
                return;
            }
            if (name) {
                if (typeUtils.isString(nameOrOptions)) {
                    table.caption && domUtils.text(table.caption, name);
                } else if (prototypeUtils.isObject(nameOrOptions)) {
                    this.initCaption(table, nameOrOptions);
                } else {
                    return table.caption && domUtils.text(table.caption);
                }
            } else {
                return table.caption && domUtils.text(table.caption);
            }
        };
        let bo = document.body || document
            .getElementsByTagName("body")[0];
        bo.insertBefore(table, bo.firstChild);
    }
    sortTbody(self, sortdata, table) {
        if (self.data.length == sortdata.length) {
            for (let index = 0; index < sortdata.length; index++) {
                const sortda = sortdata[index];
                let exists = -1;
                for (let i = 0; i < self.data.length; i++) {
                    const da = self.data[i];
                    if (da == sortda) {
                        exists = i;
                        break;
                    }
                }
                if (exists != -1 && exists != index) {
                    //let oldrow = table.rows[exists];
                    //let newrow = table.rows[index];
                    table.deleteRow(9);
                    let row = table.insertRow(index);
                    self.setCell(self, row, sortda);
                    //table.deleteRow();
                    //table.insertRow();
                    //table.rows[index] = oldrow;
                    //table.rows[exists] = newrow;
                }
            }
        }
    }
    getDeleteRowPos(da, data) {
        for (let i = 0; i < data.length; i++) {
            const da = data[i];
            if (da == da) {
                return i;
            }
        }
        return -1;
    }
    initTbody(self, table, data, sort) {
        // let tfoot = table.tFoot = table.tFoot ||
        //     table.createTFoot() || document.createElement("tfoot");
        let tbody = table.tbody || document.createElement("tbody");
        if (sort) {
            for (let index = table.rows.length - 1; index >= 0; index--) {
                table.deleteRow(index);
            }
        } else {
            //table.tFoot.append(tbody);
        }
        for (let i = 0; i < data.length; i++) {
            const da = data[i];
            let tr = self.createRow(table, i, tbody);
            self.setCell(self, tr, da);
        }
    }
    setCell(self, tr, data) {
        for (let index = 0; index < self.columns.length; index++) {
            const element = self.columns[index];
            let td = self.createCell(tr, index);
            td.align = element.align || "center";
            if (!element.column && element.checkbox) {
                let check = document.createElement("input");
                check.setAttribute("type", "checkbox");
                td.append(check);
            } else {
                let val = data[element.column];
                if (val !== undefined) {
                    val = val.toString();
                } else {
                    val = "";
                }
                if (element.input) {
                    if (prototypeUtils.isString(element.input)) {
                        let input = document.createElement("input");
                        input.setAttribute("type", element.input);
                        if (element.input == "radio") {
                            // domUtils.text(input, val);
                            td.insertBefore(document.createTextNode(val), td.lastChild);
                            input.setAttribute("checked", "checked");
                            td.insertBefore(input, td.lastChild);
                        }
                    } else if (prototypeUtils.isObject(element.input)) {

                        if (element.input.type == "radio") {
                            // domUtils.text(input, val);
                            for (let index = element.input.values.length - 1; index > -1; index--) {
                                let input = document.createElement("input");
                                input.setAttribute("type", element.input.type);
                                const vals = element.input.values[index];
                                td.insertBefore(document.createTextNode(vals), td.lastChild);
                                if (vals == val) {
                                    input.setAttribute("checked", "checked");
                                }
                                td.insertBefore(input, td.lastChild);
                            }

                        }
                    } else {
                        let input = document.createElement("input");
                        input.setAttribute("type", "text");
                        input.setAttribute("value", val);
                        td.insertBefore(input, td.lastChild);
                    }

                } else {
                    domUtils.text(td, val);
                }
            }
        }
    }
    createRow(table, pos, tbody) {
        if (table.insertRow) {
            return table.insertRow(pos);
        } else {
            let tr = document.createElement("tr");
            tbody.append(tr);
            return tr;
        }
    }
    createCell(row, pos) {
        if (row.insertCell) {
            return row.insertCell(pos);
        } else {
            let td = document.createElement("td");
            row.append(td);
            return td;
        }
    }
    deleteCell(row, pos) {
        if (row.deleteCell) {
            row.deleteCell(pos);
        } else {
            row.removeElement(pos);
        }
    }
    deleteRow(table, pos, tbody) {
        if (table.deleteRow) {
            return table.deleteRow(pos);
        } else {
            return tbody.removeElement(pos);
        }
    }
    initThead(self, table, options, data, initTbody) {
        let theadElement = (table.tHead = table.tHead || table.createTHead() ||
            document.createElement("thead"));
        if (options) {
            if (prototypeUtils.isArray(options)) {
                options.forEach(it => {
                    if (it.name || it.checkbox) {
                        var th = document.createElement("th");
                        let align = it.align || "center";
                        th.setAttribute("align", align);
                        if (it.name) {
                            domUtils.text(th, it.name);
                            if (it.sort) {
                                let span = document.createElement("span");
                                span.setAttribute("data-sort", true);
                                th.append(span);
                                let temp = [];

                                domUtils.event.addEvent(span, "click", function (event) {
                                    domUtils.toggleClass(this, "collapsed");
                                    let sort = this.getAttribute("data-sort");
                                    sort = sort == "true" ? false : true;
                                    this.setAttribute("data-sort", sort);
                                    if (temp.length == 0) {
                                        self.data.forEach(it => temp.push(it));
                                    }
                                    if (sort) {
                                        temp.sort(self.sort(it.column, sort));
                                    } else {
                                        temp.sort(compareObject(it.column, sort));
                                    }
                                    // self.sortTbody(self, temp, table);
                                    self.initTbody(self, table, temp, true);
                                });
                                domUtils.toggleClass(span, "collapse");
                            }
                        } else {
                            let check = document.createElement("input");
                            check.setAttribute("type", "checkbox");
                            th.append(check);
                        }
                        theadElement.append(th);
                    }
                });
            } else {
                throw new Error("options not is array");
            }
        }
    }
    initCaption(table, options, falg) {
        if (options) {
            table.caption = (table.caption || table.createCaption() ||
                document.createElement("caption"))
            let caption = table.caption;
            if (options.name) {
                domUtils.text(caption, options.name);
            }
            if (falg) {
                let align = options.align ? options.align : "center";
                caption.setAttribute("align", align);
            }
            for (const key in options.attrs) {
                let val = options.attrs[key];
                caption.setAttribute(key, val);
            }
        }
    }
}