// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Synapse.Addins.CodeSnippets {
    using System;
    using Qyoto;
    
    
    public partial class InsertSnippetDialog : QDialog {
        
        protected QLabel label;
        
        protected QComboBox typeComboBox;
        
        protected QLabel label_2;
        
        protected QLabel toLabel;
        
        protected QTabWidget tabWidget;
        
        protected QWidget tab_3;
        
        protected QTextEdit textEdit;
        
        protected QWidget tab_4;
        
        protected QWebView webView;
        
        protected QPushButton pushButton;
        
        protected QDialogButtonBox buttonBox;
        
        protected void SetupUi() {
            base.ObjectName = "InsertSnippetDialog";
            this.Geometry = new QRect(0, 0, 400, 300);
            this.WindowTitle = "Insert Code Snippet";
            QVBoxLayout verticalLayout;
            verticalLayout = new QVBoxLayout(this);
            verticalLayout.Margin = 6;
            QGridLayout gridLayout;
            gridLayout = new QGridLayout();
            verticalLayout.AddLayout(gridLayout);
            this.label = new QLabel(this);
            this.label.ObjectName = "label";
            this.label.Text = "Type:";
            gridLayout.AddWidget(this.label, 1, 0, 1, 1);
            this.typeComboBox = new QComboBox(this);
            this.typeComboBox.ObjectName = "typeComboBox";
            gridLayout.AddWidget(this.typeComboBox, 1, 1, 1, 1);
            QSpacerItem horizontalSpacer;
            horizontalSpacer = new QSpacerItem(40, 20, QSizePolicy.Policy.Expanding, QSizePolicy.Policy.Minimum);
            gridLayout.AddItem(horizontalSpacer, 1, 2, 1, 1);
            this.label_2 = new QLabel(this);
            this.label_2.ObjectName = "label_2";
            this.label_2.Text = "To:";
            gridLayout.AddWidget(this.label_2, 0, 0, 1, 1);
            this.toLabel = new QLabel(this);
            this.toLabel.ObjectName = "toLabel";
            this.toLabel.Text = "";
            gridLayout.AddWidget(this.toLabel, 0, 1, 1, 1);
            QSpacerItem horizontalSpacer_3;
            horizontalSpacer_3 = new QSpacerItem(40, 20, QSizePolicy.Policy.Expanding, QSizePolicy.Policy.Minimum);
            gridLayout.AddItem(horizontalSpacer_3, 0, 2, 1, 1);
            this.tabWidget = new QTabWidget(this);
            this.tabWidget.ObjectName = "tabWidget";
            this.tabWidget.CurrentIndex = 0;
            verticalLayout.AddWidget(this.tabWidget);
            this.tab_3 = new QWidget(this.tabWidget);
            this.tab_3.ObjectName = "tab_3";
            QHBoxLayout horizontalLayout;
            horizontalLayout = new QHBoxLayout(this.tab_3);
            horizontalLayout.Spacing = 0;
            horizontalLayout.Margin = 0;
            this.textEdit = new QTextEdit(this.tab_3);
            this.textEdit.ObjectName = "textEdit";
            this.textEdit.FrameShape = QFrame.Shape.NoFrame;
            horizontalLayout.AddWidget(this.textEdit);
            this.tabWidget.AddTab(this.tab_3, "Paste");
            this.tab_4 = new QWidget(this.tabWidget);
            this.tab_4.ObjectName = "tab_4";
            QHBoxLayout horizontalLayout_2;
            horizontalLayout_2 = new QHBoxLayout(this.tab_4);
            horizontalLayout_2.Spacing = 0;
            horizontalLayout_2.Margin = 0;
            this.webView = new QWebView(this.tab_4);
            this.webView.ObjectName = "webView";
            this.webView.Url = new QUrl("about:blank");
            horizontalLayout_2.AddWidget(this.webView);
            this.tabWidget.AddTab(this.tab_4, "Preview");
            QHBoxLayout horizontalLayout_3;
            horizontalLayout_3 = new QHBoxLayout();
            verticalLayout.AddLayout(horizontalLayout_3);
            this.pushButton = new QPushButton(this);
            this.pushButton.ObjectName = "pushButton";
            this.pushButton.Text = "Import File...";
            horizontalLayout_3.AddWidget(this.pushButton);
            QSpacerItem horizontalSpacer_2;
            horizontalSpacer_2 = new QSpacerItem(40, 20, QSizePolicy.Policy.Expanding, QSizePolicy.Policy.Minimum);
            horizontalLayout_3.AddItem(horizontalSpacer_2);
            this.buttonBox = new QDialogButtonBox(this);
            this.buttonBox.ObjectName = "buttonBox";
            this.buttonBox.StandardButtons = global::Qyoto.Qyoto.GetCPPEnumValue("QDialogButtonBox", "NoButton");
            horizontalLayout_3.AddWidget(this.buttonBox);
            QObject.Connect(buttonBox, Qt.SIGNAL("accepted()"), this, Qt.SLOT("accept()"));
            QObject.Connect(buttonBox, Qt.SIGNAL("rejected()"), this, Qt.SLOT("reject()"));
            QMetaObject.ConnectSlotsByName(this);
        }
    }
}
