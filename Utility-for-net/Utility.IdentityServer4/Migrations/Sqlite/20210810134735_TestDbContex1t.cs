using Microsoft.EntityFrameworkCore.Migrations;

namespace Utility.IdentityServer4.Migrations.Sqlite
{
    public partial class TestDbContex1t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClaims_ApiResources_ApiResourceId",
                table: "ApiResourceClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceProperties_ApiResources_ApiResourceId",
                table: "ApiResourceProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceScopes_ApiResources_ApiResourceId",
                table: "ApiResourceScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceSecrets_ApiResources_ApiResourceId",
                table: "ApiResourceSecrets");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeClaims_ApiScopes_ScopeId",
                table: "ApiScopeClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeProperties_ApiScopes_ScopeId",
                table: "ApiScopeProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityResourceClaims_IdentityResources_IdentityResourceId",
                table: "IdentityResourceClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityResourceProperties_IdentityResources_IdentityResourc~",
                table: "IdentityResourceProperties");

            migrationBuilder.DropIndex(
                name: "IX_IdentityResources_Name",
                table: "IdentityResources");

            migrationBuilder.DropIndex(
                name: "IX_ApiScopes_Name",
                table: "ApiScopes");

            migrationBuilder.DropIndex(
                name: "IX_ApiResources_Name",
                table: "ApiResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityResourceProperties",
                table: "IdentityResourceProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityResourceClaims",
                table: "IdentityResourceClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiScopeProperties",
                table: "ApiScopeProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiScopeClaims",
                table: "ApiScopeClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceSecrets",
                table: "ApiResourceSecrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceScopes",
                table: "ApiResourceScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceProperties",
                table: "ApiResourceProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceClaims",
                table: "ApiResourceClaims");

            migrationBuilder.RenameTable(
                name: "IdentityResourceProperties",
                newName: "IdentityResourceProperty");

            migrationBuilder.RenameTable(
                name: "IdentityResourceClaims",
                newName: "IdentityResourceClaim");

            migrationBuilder.RenameTable(
                name: "ApiScopeProperties",
                newName: "ApiScopeProperty");

            migrationBuilder.RenameTable(
                name: "ApiScopeClaims",
                newName: "ApiScopeClaim");

            migrationBuilder.RenameTable(
                name: "ApiResourceSecrets",
                newName: "ApiResourceSecret");

            migrationBuilder.RenameTable(
                name: "ApiResourceScopes",
                newName: "ApiResourceScope");

            migrationBuilder.RenameTable(
                name: "ApiResourceProperties",
                newName: "ApiResourceProperty");

            migrationBuilder.RenameTable(
                name: "ApiResourceClaims",
                newName: "ApiResourceClaim");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityResourceProperties_IdentityResourceId",
                table: "IdentityResourceProperty",
                newName: "IX_IdentityResourceProperty_IdentityResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityResourceClaims_IdentityResourceId",
                table: "IdentityResourceClaim",
                newName: "IX_IdentityResourceClaim_IdentityResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiScopeProperties_ScopeId",
                table: "ApiScopeProperty",
                newName: "IX_ApiScopeProperty_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiScopeClaims_ScopeId",
                table: "ApiScopeClaim",
                newName: "IX_ApiScopeClaim_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceSecrets_ApiResourceId",
                table: "ApiResourceSecret",
                newName: "IX_ApiResourceSecret_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceScopes_ApiResourceId",
                table: "ApiResourceScope",
                newName: "IX_ApiResourceScope_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceProperties_ApiResourceId",
                table: "ApiResourceProperty",
                newName: "IX_ApiResourceProperty_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceClaims_ApiResourceId",
                table: "ApiResourceClaim",
                newName: "IX_ApiResourceClaim_ApiResourceId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdentityResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "IdentityResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "IdentityResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiScopes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApiScopes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiScopes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApiResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AllowedAccessTokenSigningAlgorithms",
                table: "ApiResources",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "IdentityResourceProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "IdentityResourceProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "IdentityResourceClaim",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiScopeProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiScopeProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiScopeClaim",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiResourceSecret",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldMaxLength: 4000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiResourceSecret",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiResourceSecret",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                table: "ApiResourceScope",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiResourceProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiResourceProperty",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiResourceClaim",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityResourceProperty",
                table: "IdentityResourceProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityResourceClaim",
                table: "IdentityResourceClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiScopeProperty",
                table: "ApiScopeProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiScopeClaim",
                table: "ApiScopeClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceSecret",
                table: "ApiResourceSecret",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceScope",
                table: "ApiResourceScope",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceProperty",
                table: "ApiResourceProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceClaim",
                table: "ApiResourceClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClaim_ApiResources_ApiResourceId",
                table: "ApiResourceClaim",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceProperty_ApiResources_ApiResourceId",
                table: "ApiResourceProperty",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceScope_ApiResources_ApiResourceId",
                table: "ApiResourceScope",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceSecret_ApiResources_ApiResourceId",
                table: "ApiResourceSecret",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeClaim_ApiScopes_ScopeId",
                table: "ApiScopeClaim",
                column: "ScopeId",
                principalTable: "ApiScopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeProperty_ApiScopes_ScopeId",
                table: "ApiScopeProperty",
                column: "ScopeId",
                principalTable: "ApiScopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityResourceClaim_IdentityResources_IdentityResourceId",
                table: "IdentityResourceClaim",
                column: "IdentityResourceId",
                principalTable: "IdentityResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityResourceProperty_IdentityResources_IdentityResourceId",
                table: "IdentityResourceProperty",
                column: "IdentityResourceId",
                principalTable: "IdentityResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClaim_ApiResources_ApiResourceId",
                table: "ApiResourceClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceProperty_ApiResources_ApiResourceId",
                table: "ApiResourceProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceScope_ApiResources_ApiResourceId",
                table: "ApiResourceScope");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceSecret_ApiResources_ApiResourceId",
                table: "ApiResourceSecret");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeClaim_ApiScopes_ScopeId",
                table: "ApiScopeClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiScopeProperty_ApiScopes_ScopeId",
                table: "ApiScopeProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityResourceClaim_IdentityResources_IdentityResourceId",
                table: "IdentityResourceClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityResourceProperty_IdentityResources_IdentityResourceId",
                table: "IdentityResourceProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityResourceProperty",
                table: "IdentityResourceProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityResourceClaim",
                table: "IdentityResourceClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiScopeProperty",
                table: "ApiScopeProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiScopeClaim",
                table: "ApiScopeClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceSecret",
                table: "ApiResourceSecret");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceScope",
                table: "ApiResourceScope");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceProperty",
                table: "ApiResourceProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiResourceClaim",
                table: "ApiResourceClaim");

            migrationBuilder.RenameTable(
                name: "IdentityResourceProperty",
                newName: "IdentityResourceProperties");

            migrationBuilder.RenameTable(
                name: "IdentityResourceClaim",
                newName: "IdentityResourceClaims");

            migrationBuilder.RenameTable(
                name: "ApiScopeProperty",
                newName: "ApiScopeProperties");

            migrationBuilder.RenameTable(
                name: "ApiScopeClaim",
                newName: "ApiScopeClaims");

            migrationBuilder.RenameTable(
                name: "ApiResourceSecret",
                newName: "ApiResourceSecrets");

            migrationBuilder.RenameTable(
                name: "ApiResourceScope",
                newName: "ApiResourceScopes");

            migrationBuilder.RenameTable(
                name: "ApiResourceProperty",
                newName: "ApiResourceProperties");

            migrationBuilder.RenameTable(
                name: "ApiResourceClaim",
                newName: "ApiResourceClaims");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityResourceProperty_IdentityResourceId",
                table: "IdentityResourceProperties",
                newName: "IX_IdentityResourceProperties_IdentityResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityResourceClaim_IdentityResourceId",
                table: "IdentityResourceClaims",
                newName: "IX_IdentityResourceClaims_IdentityResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiScopeProperty_ScopeId",
                table: "ApiScopeProperties",
                newName: "IX_ApiScopeProperties_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiScopeClaim_ScopeId",
                table: "ApiScopeClaims",
                newName: "IX_ApiScopeClaims_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceSecret_ApiResourceId",
                table: "ApiResourceSecrets",
                newName: "IX_ApiResourceSecrets_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceScope_ApiResourceId",
                table: "ApiResourceScopes",
                newName: "IX_ApiResourceScopes_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceProperty_ApiResourceId",
                table: "ApiResourceProperties",
                newName: "IX_ApiResourceProperties_ApiResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_ApiResourceClaim_ApiResourceId",
                table: "ApiResourceClaims",
                newName: "IX_ApiResourceClaims_ApiResourceId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdentityResources",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "IdentityResources",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "IdentityResources",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiScopes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApiScopes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiScopes",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApiResources",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApiResources",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiResources",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AllowedAccessTokenSigningAlgorithms",
                table: "ApiResources",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "IdentityResourceProperties",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "IdentityResourceProperties",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "IdentityResourceClaims",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiScopeProperties",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiScopeProperties",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiScopeClaims",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiResourceSecrets",
                type: "varchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiResourceSecrets",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApiResourceSecrets",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                table: "ApiResourceScopes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ApiResourceProperties",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiResourceProperties",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ApiResourceClaims",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityResourceProperties",
                table: "IdentityResourceProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityResourceClaims",
                table: "IdentityResourceClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiScopeProperties",
                table: "ApiScopeProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiScopeClaims",
                table: "ApiScopeClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceSecrets",
                table: "ApiResourceSecrets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceScopes",
                table: "ApiResourceScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceProperties",
                table: "ApiResourceProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiResourceClaims",
                table: "ApiResourceClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResources_Name",
                table: "IdentityResources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopes_Name",
                table: "ApiScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiResources_Name",
                table: "ApiResources",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClaims_ApiResources_ApiResourceId",
                table: "ApiResourceClaims",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceProperties_ApiResources_ApiResourceId",
                table: "ApiResourceProperties",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceScopes_ApiResources_ApiResourceId",
                table: "ApiResourceScopes",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceSecrets_ApiResources_ApiResourceId",
                table: "ApiResourceSecrets",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeClaims_ApiScopes_ScopeId",
                table: "ApiScopeClaims",
                column: "ScopeId",
                principalTable: "ApiScopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiScopeProperties_ApiScopes_ScopeId",
                table: "ApiScopeProperties",
                column: "ScopeId",
                principalTable: "ApiScopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityResourceClaims_IdentityResources_IdentityResourceId",
                table: "IdentityResourceClaims",
                column: "IdentityResourceId",
                principalTable: "IdentityResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityResourceProperties_IdentityResources_IdentityResourc~",
                table: "IdentityResourceProperties",
                column: "IdentityResourceId",
                principalTable: "IdentityResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
