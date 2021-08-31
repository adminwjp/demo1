package template

type PropertyDto struct {
	PrivatePropertyName string
	PropertyName string
	ColumnName string
	RequestFlag int
	MappFlag int
	Identity bool
	Length int
	ValueType bool
	FkColumnName string
	ConstiantName string
	ReferenceTable string
	Comment string
	Single bool
	PropertyType string
	ReferencePropertyName  string
}

type ClassDto struct {
	ClassName string
	TableName string
}

