param([Switch]$Release)

Import-Module "$PSScriptRoot\..\TestFramework.psm1" -Force
$ModulePath = Get-ModulePath -Release:$Release
Import-Module $ModulePath -Force
Describe "New-UDImage" {
    It "should support path parameter" {
        $Image = New-UDImage -Path http://www.google.com/image.png 
        $Image.Attributes['src'] | Should be 'http://www.google.com/image.png'
    }
}