class Table {
    constructor(options) {
        options = options || {};
        this.options = options;
        //Ê÷ÐÎÕÛµþ²Ëµ¥Í¼±ê
        this.options.collapsed = { close: "<i class=\"fa fa-angle-down\" aria-hidden=\"true\"></i>", open: "<i class=\"fa fa-angle-up\" aria-hidden=\"true\"></i>" };
        //ÅÅÐòÍ¼±ê
        this.options.sort = { asc: "<i class=\"fa fa-chevron-circle-up\" aria-hidden=\"true\"></i>", desc: "<i class=\"fa fa-chevron-circle-down\" aria-hidden=\"true\"></i>" };
        this.columns = options.columns;
        this.data = options.data;
        this.sort = null;
        let table = document.getElementById(options.id);//document.createElement("table");
        if (options.caption && options.caption.name) {
            this.initCaption(table, options.caption);
        }
        this.initThead(this, table, options.columns);
        //this.initTbody(this, table, options.data);
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
        //let bo = document.body || document.getElementsByTagName("body")[0];
        //bo.insertBefore(table, bo.firstChild);
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
    /**
     * ininial table column info
     * @param {any} self
     * @param {any} table
     * @param {any} options
     */
    initThead(self, table, options) {
        let theadElement = (table.tHead = table.tHead || table.createTHead() ||
            document.createElement("thead"));
        if (options) {
            if (prototypeUtils.isArray(options)) {
                options.forEach(it => {
                    if (it.name) {
                        var th = document.createElement("th");
                        th.setAttribute("align", it.align || "center");
          
                        th.setAttribute("column", it.column);
                        if (it.name) {
                            domUtils.text(th, it.name);
                            if (it.sort) {
                                th.setAttribute("data-sort", true);
                                domUtils.html(th, self.options.sort.asc);
                                let temp = [];
                                let sortClickEvent = this.sortEvent(self, table, options);
                                domUtils.event.removeEvent(th, "click", sortClickEvent);
                                domUtils.event.addEvent(th, "click", sortClickEvent);
                                //domUtils.toggleClass(span, "collapse");
                            }
                        }
                        theadElement.append(th);
                    }
                });
            } else {
                throw new Error("columns  is not array");
            }
        }
    }
    sortEvent(self, table, options) {
        let temp = [];
        return function (event) {
            //domUtils.toggleClass(this, "collapsed");
            let sort = this.getAttribute("data-sort");
            sort = sort == "true" ? false : true;
            domUtils.html(span, sort ? self.options.sort.asc : self.options.sort.desc);
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
        };
    }
 
    /**
     * ininial table title
     * @param {any} table
     * @param {any} options
     */
    initCaption(table, options) {
        this.throwException(table);
        if (options) {
            table.caption = table.caption || table.createCaption() || document.createElement("caption")
            let caption = table.caption;
            if (options.name) {
                domUtils.text(caption, options.name);
                let align = options.align ? options.align : "center";
                caption.setAttribute("align", align);
            }
            for (const key in options.attrs) {
                let val = options.attrs[key];
                caption.setAttribute(key, val);
            }
        }
    }
    throwException(table) {
        if (!table)
            throw new Error("table element not exists");
    }
}