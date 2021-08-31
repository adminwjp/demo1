package template

type GeneratorHelper struct {
	
}

type ClassResovler struct {

}

type PropertyResovler struct {

}

var CsharpTypes=make(map[string]string,100)

func initCsharpTypes()  {
	CsharpTypes["string"]="string"
	CsharpTypes["char"]="char"
	CsharpTypes["byte"]="byte"
	CsharpTypes["int"]="int"
	CsharpTypes["short"]="short"
	CsharpTypes["long"]="long"
	CsharpTypes["float"]="float"
	CsharpTypes["double"]="double"
	CsharpTypes["decimal"]="decimal"

	CsharpTypes["bool"]="bool"
	CsharpTypes["DateTime"]="DateTime"

	CsharpTypes["char?"]="char?"
	CsharpTypes["byte?"]="byte?"
	CsharpTypes["int?"]="int?"
	CsharpTypes["short?"]="short?"
	CsharpTypes["long?"]="long?"
	CsharpTypes["float?"]="float?"
	CsharpTypes["double?"]="double?"
	CsharpTypes["decimal?"]="decimal?"

	CsharpTypes["bool?"]="bool?"
	CsharpTypes["DateTime?"]="DateTime?"
}
