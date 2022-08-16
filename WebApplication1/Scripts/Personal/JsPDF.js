
    //<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

    //<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js"></script>

    //<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/2.0.37/jspdf.plugin.autotable.js"></script>

//export table to excel
function generateExcel(data) {
    //getting data from our table
    var data_type = 'data:application/vnd.ms-excel';
    var table_div = document.getElementById(data);
    var table_html = table_div.outerHTML.replace(/ /g, '%20');

    var a = document.createElement('a');
    a.href = data_type + ', ' + table_html;
    a.download = 'Example_Table_To_Excel.xls';
    a.click();
}


//export table to pdf
function generatePDF(data) {
    debugger
    var doc = new jsPDF('l', 'pt');

    var elem = document.getElementById(data);
    var data = doc.autoTableHtmlToJson(elem);
    doc.autoTable(data.columns, data.rows, {
        margin: { left: 35 },
        theme: 'grid',
        tableWidth: 'auto',
        fontSize: 8,
        overflow: 'linebreak',
    }
    );

    doc.save('Example_Table_To_PDF.pdf');
}


    //<button onclick="generateExcel('table_with_data')">Export to Excel</button>
    //<button onclick="generatePDF('table_with_data')">Export to PDF</button>